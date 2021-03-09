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
    public List<GameObject> riddleSpots; // all the RiddleSpots in the game in room order.
    public int[] answers = new int[10] {0,0,0,0,0,0,0,0,0,0}; // player's answers to the riddles, in room order
    public List<Doors> doors; // the doors currently in the scene

    [Header("Set Dynamically")]
    public bool hasSeenZeus = false; // if the player has seen zeus to get the quest.
    public bool isPaused = false; // is the player paused right now (in the menu area)
    public bool isPlayerActive = true; // false if the player being teleported right now
    // public bool recentre = false; // should we recentre the local positions of vr player/camera
    public int prevScene = -1;
    public int newScene = -1;
    public int numCollectables = 0;
    public Vector3 teleportLocation; // where are we going?
    public Vector3 teleportRotation; // where are we facing?
    public Vector3 underTeleport; // underworld teleport location
    public Vector3 hadesTeleport; // hades room teleport location
    public float playerStartY; // player's y position (so they don't become real short or hella tall after teleport)
    public Vector3 playerLoadLocation = new Vector3(0f,1.6f,0f); // the location the player should move to on scene load
    public Vector3 playerLoadRotation = new Vector3(0f,0f,0f); // the rotation the player should move to on scene load
    public int playerInRoom = 0;
    
    // Private Vars

    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            // grab the player's y position for teleportion reasons
            playerStartY = ovrPlayer.transform.position.y;
            if(!PlayerPrefs.HasKey("loadGame"))
            {
                Debug.Log("Something went wrong. Could not find player prefs");
            }
            else
            {
                if(PlayerPrefs.GetInt("loadGame") == 1)
                {
                    Invoke("Load", 1f);
                }
            }
            DontDestroyOnLoad(this);
        }
    }

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

            InventoryManager.Instance.SetInventory(save.inventory);
            PuzzleManager.Instance.puzzleStatus = save.puzzleStatus;
            hasSeenZeus = save.hasSeenZeus;
            prevScene = 0;
            newScene = 0;

            Debug.Log("Game Loaded");
            MenuManager.Instance.Unpause();
        }
        else
        {
            Debug.Log("No Game Saved!");
        }
    }

   public void StartTeleport(string place) //Assign this in the menu button.
    {
        isPlayerActive = false;
        switch (place) {
            case "Underworld":
                teleportLocation = new Vector3(hadesTeleport.x, playerStartY, hadesTeleport.z); // if on underworld teleport go up to hades room
                playerInRoom = 6;
                break;
            case "Hades":
                playerStartY = ovrPlayer.transform.position.y;
                teleportLocation = new Vector3(underTeleport.x, underTeleport.y + playerStartY, underTeleport.z); // if on hades teleport go to underworld
                playerInRoom = 7;
                break;
            case "maze":
                teleportLocation = MazePuzzle.Instance.startOfMaze;
                break;
            case "startMaze":
                teleportLocation = MazePuzzle.Instance.returnFromMaze;
                MazePuzzle.Instance.hasLost = true;
                break;
            case "endMaze":
                teleportLocation = MazePuzzle.Instance.returnFromMaze;
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
        // recentre = true;
    }

    public void ResetForNextScene(int scene)
    {
        doors.Clear();
        riddleSpots.Clear();
        ovrPlayer = null;
        cameraRig = null;
        InventoryManager.Instance.roomCollectable = null;
        prevScene = newScene;
        newScene = scene;
    }

    public void ChangeSceneTo(int scene, Vector3 location, Vector3 rotation)
    {
        Debug.Log(scene + " <- scene   location ->  " + location + "   rotation -> " + rotation);
        ResetForNextScene(scene);
        playerInRoom = scene;
        if(scene != 10)
        {
            // we're going to the menu
            playerStartY = ovrPlayer.transform.position.y;
        }
        playerLoadLocation = location;
        playerLoadRotation = rotation;
        // playerLoadLocation = new Vector3(1.01f,playerStartY,12.96f);
        // playerLoadRotation = new Vector3(0f,180f,0f);

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
                Debug.Log("Something went wrong. scene not exist" + scene);
                break;
        }

    }
    
    void Update()
    {
        // if the player hit the menu button, teleport them to menu and pause the game.
        if(!isPaused && ((Input.GetKeyDown(KeyCode.Escape) || OVRInput.Get(OVRInput.Button.Start))))
        {
            isPaused = true;
            ChangeSceneTo(10, playerLoadLocation, playerLoadRotation);
        }
        if(Input.GetKeyDown(KeyCode.Return) || OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) >= 0.5f)
        {
            Debug.Log("Wants to Change Scene");
            foreach(Doors door in doors)
            {
                if(door.isColliding)
                {
                    ChangeSceneTo(door.toRoomNum, door.playerLoadLocation, door.playerLoadRotation);
                    door.isColliding = false;
                    break;
                }
            }
        }
    }

    private Save CreateSave()
    {
        // save data in the Save script for binary formatting.
        Save save = new Save();
        save.inventory = InventoryManager.Instance.GetInventory();
        save.puzzleStatus = PuzzleManager.Instance.puzzleStatus;
        save.hasSeenZeus = hasSeenZeus;
        return save;
    }
}
