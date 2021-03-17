using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;


// REFERENCES
// Text Riddle Answers: start = a/b, hephaistos = a, aphrodite = a, kharities = b, inventory/pandora, hera = a, athena = b, hades = b, hermes = b, zeus = a, underworld = a
// RoomNums: 0 = Start, 1 = Hephaistos, 2 = Aphrodite, 3 = Kharities, 4 = Hera, 5 = Athena, 6 = Hades/Underworld, 8 = Hermes, 9 = Zeus, 10 = Pandora/Inventory, 11 = Menu, 12 = Hallway
// Collectables: Dirt, Water, Clothing, Grace, Jewellery, Flowers, Wovens, Deceit, Box
// River Puzzle Solution: Charon Takes H, Returns alone, Takes either L or D, Returns with H, takes other (either L or D), Returns alone, Takes H
// Simon Says Colour Buttons: 1 = Pink, 2 = Orange, 3 = Yellow, 4 = Green, 5 = Blue, 6 = Purple, 7 = White, 8 = Black
// Puzzle Points Array Order: 0 = Maze, 1 = River Puzzle, 2 = Simon Says, 3 = Questions

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // only want one GameManager

    [Header("G A M E  M A N A G E R")]
    [Header("Set In Inspector")]
    public GameObject ovrPlayer; // vr player
    public GameObject cameraRig; // vr camera
    public int[] answers = new int[10] {0,0,0,0,0,0,0,0,0,0}; // player's answers to the riddles, in room order
    public List<Doors> doors; // the doors currently in the scene

    [Header("Set Dynamically")]
    public bool hasSeenZeus = false; // if the player has seen zeus to get the quest.
    public bool isPaused = false; // is the player paused right now (in the menu area)
    public bool isPlayerActive = true; // false if the player being teleported right now
    public bool recentre = false; // should we recentre the local positions of vr player/camera
    public int prevScene = -1;
    public int newScene = 0;
    public int numCollectables = 0;
    public Vector3 teleportLocation; // where are we going?
    public Vector3 teleportRotation; // where are we facing?
    public float playerStartY = 0.0f; // player's y position (so they don't become real short or hella tall after teleport)
    public Vector3 playerLoadLocation = new Vector3(0f,1.6f,0f); // the location the player should move to on scene load
    public Vector3 playerLoadRotation = new Vector3(0f,0f,0f); // the rotation the player should move to on scene load
    public int playerInRoom = 0;
    public GameObject roomCollectable;

    // Private Vars
    private int[] correctAnswers = new int[10]{0,1,1,2,1,2,2,1,2,1}; // the correct answers to the questions in Athena(5)
    private List<Collectables> inventory; // player's current collected collectables


    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            // grab the player's y position for teleportion reasons
            playerStartY = ovrPlayer.transform.position.y;
            // set new inventory
            inventory = new List<Collectables>();
            DontDestroyOnLoad(this);
        }
    }
    
    void Update()
    {
        // if the player hit the menu button, teleport them to menu and pause the game.
        if(!isPaused && ((Input.GetKeyDown(KeyCode.Escape) || OVRInput.Get(OVRInput.Button.Start))))
        {
            if(!TitleScreen.Instance)
            {
                isPaused = true;
                ChangeSceneTo(11, playerLoadLocation, playerLoadRotation);
            }
        }
        // recenter local positions if necessary
        // if(recentre)
        // {

        //     ovrPlayer.transform.localPosition = new Vector3(0, ovrPlayer.transform.localPosition.y, 0);
        //     cameraRig.transform.localPosition = new Vector3(0, cameraRig.transform.localPosition.y, 0);
        //     recentre = false;            
        // }
    }

    //* UTILITIES
    public bool CheckAnswers()
    {
        int correct = 1;
        for(int i = 1; i < correctAnswers.Length; i++)
        {
            if(correctAnswers[i] == answers[i])
            {
                correct++;
            }
        }
        if(correct > 6)
        {
            roomCollectable.SetActive(true);
        }
        return correct > 6;
    }

    //* INVENTORY
    public void AddToInventory(Collectables type)
    {
        if(!isInInventory(type))
        {
            inventory.Add(type);
            roomCollectable.SetActive(false);
        }
    }

    public void RemoveFromInventory(Collectables type)
    {
        int i = inventory.IndexOf(type);
        inventory.RemoveAt(i);
    }
    
    public List<Collectables> GetInventory()
    {
        return inventory;
    }

    public bool isInInventory(Collectables type)
    {
        return inventory.Contains(type);
    }

    public void SetInventory(List<Collectables> inven)
    {
        // called when loading saved data, update list, and active
        inventory = inven;
    }

    //* TELEPORTATION AND ROOM SCENE CHANGE
       public void StartTeleport(string place) //Assign this in the menu button.
    {
        isPlayerActive = false;
        switch (place) {
            case "maze":
                teleportLocation = MazePuzzle.Instance.startOfMaze;
                teleportRotation = MazePuzzle.Instance.rotationOfMaze;
                MazePuzzle.Instance.inMaze = true;
                break;
            case "startMaze":
                teleportLocation = MazePuzzle.Instance.returnFromMaze;
                teleportRotation = MazePuzzle.Instance.rotationFromMaze;
                MazePuzzle.Instance.hasLost = true;
                break;
            case "endMaze":
                teleportLocation = MazePuzzle.Instance.returnFromMaze;
                teleportRotation = MazePuzzle.Instance.rotationFromMaze;
                MazePuzzle.Instance.hasWon = true;
                break;
            default:
                Debug.Log("Invalid Teleport");
                return;
        }
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.teleportClip);

        StartCoroutine(Teleport());
    }

    public void StartAt()
    {
        isPlayerActive = false;
        playerLoadLocation.y = playerStartY;
        teleportLocation = playerLoadLocation;
        teleportRotation = playerLoadRotation;
        StartCoroutine(Teleport());
    }


    public IEnumerator Teleport()
    {
        // after half a second, move player to new location and restore movement
        yield return new WaitForSeconds(0.5f);
        ovrPlayer.transform.position = teleportLocation;
        ovrPlayer.transform.localRotation = Quaternion.identity;
        ovrPlayer.transform.Rotate(teleportRotation);
        isPlayerActive = true;
        // if(MazePuzzle.Instance.hasWon || MazePuzzle.Instance.hasLost || teleportLocation.y > 90.0f)
        // {
        //     recentre = true;
        // }
    }

    public void ResetForNextScene(int scene)
    {
        doors.Clear();
        ovrPlayer = null;
        cameraRig = null;
        roomCollectable = null;
        prevScene = newScene;
        newScene = scene;
    }

    public void ChangeSceneTo(int scene, Vector3 location, Vector3 rotation)
    {
        // Debug.Log(scene + " <- scene   location ->  " + location + "   rotation -> " + rotation);

        playerInRoom = scene;
        if(newScene != 11)
        {
            // if we are coming back from the menu, ovr player has not been set, so we don't want to access it.
            playerStartY = ovrPlayer.transform.position.y;
        }
        playerLoadLocation = location;
        playerLoadRotation = rotation;
        ResetForNextScene(scene);
        switch(scene)
        {
            case 0:
                SceneManager.LoadScene("StartRoom");
                break;
            case 1:
                SceneManager.LoadScene("HephaistosRoom");
                break;
            case 2:
                SceneManager.LoadScene("AphroditeRoom");
                break;
            case 3:
                SceneManager.LoadScene("KharitiesRoom");
                break;
            case 4:
                SceneManager.LoadScene("HeraRoom");
                break;
            case 5:
                SceneManager.LoadScene("AthenaRoom");
                break;
            case 6:
                SceneManager.LoadScene("HadesRoom");
                break;
            case 7:
                SceneManager.LoadScene("Underworld");
                break;
            case 8:
                SceneManager.LoadScene("HermesRoom");
                break;
            case 9:
                SceneManager.LoadScene("ZeusRoom");
                break;
            case 10:
                SceneManager.LoadScene("PandoraRoom");
                break;
            case 11:
                SceneManager.LoadScene("Menu");
                break;
            case 12:
                SceneManager.LoadScene("Hallway");
                break;
            default:
                Debug.Log("Something went wrong. scene does not exist" + scene);
                break;
        }
    }

    //* PERSISTENT DATA MANAGEMENT
        public void Save()
    {
        // turn data into binary format and save to persistent data
        Save save = CreateSave();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Game Saved");
    }

    public void Load()
    {
        // if player has previously saved, there will be persistent data, and we can pull it.
        if(File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            //loading = true;
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save) bf.Deserialize(file);
            file.Close();

            SetInventory(save.inventory);
            PuzzleManager.Instance.puzzleStatus = save.puzzleStatus;
            hasSeenZeus = save.hasSeenZeus;
            prevScene = -1;
            newScene = 0;

            if(playerInRoom == 11)
            {
                MenuManager.Instance.Unpause();
            }
            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.Log("No Game Saved!");
        }
    }

    private Save CreateSave()
    {
        // save data in the Save script for binary formatting.
        Save save = new Save();
        save.inventory = inventory;
        save.puzzleStatus = PuzzleManager.Instance.puzzleStatus;
        save.hasSeenZeus = hasSeenZeus;
        return save;
    }
}
