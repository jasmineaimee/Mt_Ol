using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;


// REFERENCES
// Text Riddle Answers: start = a/b, hephaistos = a, aphrodite = a, kharities = b, inventory/pandora, hera = a, athena = b, hades = b, hermes = b, zeus = a, underworld = a
// RoomNums: 0 = Start, 1 = Hephaistos, 2 = Aphrodite, 3 = Kharities, 4 = Hera, 5 = Athena, 6 = Hades/Underworld, 7 = Hermes, 8 = Zeus, 9 = Pandora/Inventory, 10 = Menu, 11 = Hallway
// DoorNums: 0 = Start, 11 = Hesphaistos West, 12 = Hephaistos East, 2 = Aphrodite, 3 = Kharities, 4 = Hera, 5 = Athena, 61 = Hades North, 62, = Hades East, 81 = Hermes South, 82 = Hermes North, 91 = Zeus South, 92 = Zeus East
// Collectables: Dirt, Water, Clothing, Grace, Jewellery, Flowers, Wovens, Deceit, Box
// River Puzzle Solution: Charon Takes H, Returns alone, Takes either L or D, Returns with H, takes other (either L or D), Returns alone, Takes H
// Simon Says Colour Buttons: 1 = Pink, 2 = Orange, 3 = Yellow, 4 = Green, 5 = Blue, 6 = Purple, 7 = White, 8 = Black
// Puzzle Points Array Order: 0 = Maze, 1 = River Puzzle

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
    public bool isPaused = false; // is the player paused right now (in the menu area)
    public bool isPlayerActive = true; // false if the player being teleported right now
    public bool recentre = false; // should we recentre the local positions of vr player/camera
    public int prevScene = -1;
    public int newScene = -1;
    public Vector3 teleportLocation; // where are we going?
    public Vector3 underTeleport; // underworld teleport location
    public Vector3 hadesTeleport; // hades room teleport location
    public float playerStartY; // player's y position (so they don't become real short or hella tall after teleport)
    public Vector3 playerLoadLocation; // the location the player should move to on scene load
    
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
            answers = save.answers;
            prevScene = -1;
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
        if(place == "Underworld")
        {
            teleportLocation = new Vector3(hadesTeleport.x, playerStartY, hadesTeleport.z); // if on underworld teleport go up to hades room
        }
        else if(place == "Hades")
        {
            playerStartY = ovrPlayer.transform.position.y;
            teleportLocation = new Vector3(underTeleport.x, underTeleport.y + playerStartY, underTeleport.z); // if on hades teleport go to underworld
        }
        else
        {
            Debug.Log("Invalid Teleport");
            return;
        }
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.teleportClip);

        StartCoroutine(Teleport());
    }

    public void StartAt()
    {
        isPlayerActive = false;
        teleportLocation = playerLoadLocation;
        StartCoroutine(Teleport());
    }


    public IEnumerator Teleport()
    {
        // after half a second, move player to new location and restore movement
        yield return new WaitForSeconds(0.5f);
        ovrPlayer.transform.position = teleportLocation;
        ovrPlayer.transform.rotation = Quaternion.identity;
        isPlayerActive = true;
        recentre = true;
    }

    public void ControlMovement(bool enableMove)
    {
        // set if player can move right now
        ovrPlayer.GetComponent<OVRPlayerController>().EnableLinearMovement = enableMove;
    }

    public void ResetForNextScene()
    {

    }

    public void ChangeSceneTo(int scene, int location)
    {
        playerStartY = ovrPlayer.transform.position.y;
        switch(location)
        {
            case -1:
                Debug.Log("This does nothing");
                break;
            default:
                Debug.Log("Something went wrong. Invalid Location" + location);
                break;
        }
        switch(scene)
        {
            case 0:
                SceneManager.LoadScene("StartRoom");
                break;
            default:
                Debug.Log("Something went wrong. scene not exist" + scene);
                break;
        }

    }
    
    void Update()
    {
        // check if player is active and recenter local positions if necessary
        ovrPlayer.GetComponent<CharacterController>().enabled = isPlayerActive;
        if(recentre)
        {

            ovrPlayer.transform.localPosition = new Vector3(0, ovrPlayer.transform.localPosition.y, 0);
            cameraRig.transform.localPosition = new Vector3(0, cameraRig.transform.localPosition.y, 0);
            recentre = false;            
        }
        // if the player hit the menu button, teleport them to menu and pause the game.
        if((Input.GetKeyDown(KeyCode.Escape) || OVRInput.Get(OVRInput.Button.Start)))
        {
            isPaused = true;
            prevScene = newScene;
            newScene = 10;
        }
    }

    private Save CreateSave()
    {
        // save data in the Save script for binary formatting.
        Save save = new Save();
        save.inventory = InventoryManager.Instance.GetInventory();
        save.answers = answers;
        return save;
    }
}
