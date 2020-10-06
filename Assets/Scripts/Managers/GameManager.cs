using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;


// REFERENCES
// Text Riddle Answers: start = a/b, hephaistos = a, aphrodite = a, kharities = b, inventory/pandora, hera = a, athena = b, hades = b, hermes = b, zeus = a, underworld = a
// RoomNums: 0 = Start, 1 = Hephaistos, 2 = Aphrodite, 3 = Kharities, 4 = Hera, 5 = Athena, 6 = Hades, 7 = Underworld, 8 = Hermes, 9 = Zeus
// Collectables: Dirt, Water, Clothing, Grace, Jewellery, Flowers, Wovens, Deceit, Box

public class GameManager : MonoBehaviour
{
    [Header("G A M E  M A N A G E R")]
    [Header("Set In Inspector")]
    public GameObject ovrPlayer; // vr player
    public GameObject cameraRig; // vr camera
    public GameObject[] riddleSpots; // all the RiddleSpots in the game in room order.
    public int[] answers = new int[10] {0,0,0,0,0,0,0,0,0,0}; // player's answers to the riddles, in room order
    public GameObject[] doors; // all the doors in the game, in room order
    public GameObject pandoraDoor; // inventory room door
    public Material wood; // door material


    [Header("Set Dynamically")]
    public static GameManager Instance; // only want one GameManager
    public bool isPlayerActive = true; // false if the player being teleported right now
    public bool recentre = false; // should we recentre the local positions of vr player/camera
    public Vector3 teleportLocation; // where are we going?
    public Vector3 underTeleport; // underworld teleport location
    public Vector3 hadesTeleport; // hades room teleport location
    public Vector3 menuTeleport; // menu player positon
    public float playerStartY; // player's y position (so they don't become real short or hella tall after teleport)
    
    
    // Private Vars
    private Vector3 loadPos = Vector3.zero;
    //private bool loading = false;

    void Start()
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

            loadPos = new Vector3(save.playerPositionX, save.playerPositionY, save.playerPositionZ);
            if(loadPos.y < 0)
            {
                loadPos = hadesTeleport;
            }
            StartTeleport("Load");

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
        if(place == "Menu")
        {
            playerStartY = ovrPlayer.transform.position.y;
            teleportLocation = new Vector3(menuTeleport.x, playerStartY, menuTeleport.z); // if pressed start teleport to the menu area
        }
        else if(place == "Load")
        {
            teleportLocation = loadPos; // if loading a game teleport to the location saved.
        }
        else if(place == "Back")
        {
            playerStartY = ovrPlayer.transform.position.y;
            teleportLocation = new Vector3(MenuManager.Instance.prevLoc.x, playerStartY, MenuManager.Instance.prevLoc.z); // if in menu area (pressed resume) teleport to where they were when they pressed start
        }
        else if(place == "Underworld")
        {
            teleportLocation = new Vector3(hadesTeleport.x, playerStartY, hadesTeleport.z); // if on underworld teleport go up to hades room
        }
        else if(place == "Hades")
        {
            playerStartY = ovrPlayer.transform.position.y;
            teleportLocation = new Vector3(underTeleport.x, underTeleport.y + playerStartY, underTeleport.z); // if on hades teleport go to underworld
        }
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.teleportClip);

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

    public void ChangeDoorMaterial(int num)
    {
        // riddle has been answered, change door material.
        switch(num)
        {
            case 0:
                doors[0].GetComponent<Renderer>().material = wood;
                break;
            case 1:
                doors[1].GetComponent<Renderer>().material = wood;
                doors[2].GetComponent<Renderer>().material = wood;
                break;
            case 2:
                doors[3].GetComponent<Renderer>().material = wood;
                break;
            case 3:
                doors[4].GetComponent<Renderer>().material = wood;
                break;
            case 4:
                doors[5].GetComponent<Renderer>().material = wood;
                break;
            case 5:
                doors[6].GetComponent<Renderer>().material = wood;
                break;
            case 6:
                doors[7].GetComponent<Renderer>().material = wood;
                doors[8].GetComponent<Renderer>().material = wood;
                break;
            case 8:
                doors[9].GetComponent<Renderer>().material = wood;
                doors[10].GetComponent<Renderer>().material = wood;
                break;
            case 9:
                doors[11].GetComponent<Renderer>().material = wood;
                doors[12].GetComponent<Renderer>().material = wood;
                break;
            default:
                Debug.Log("No door to change or out of bounds. " + num);
                break;
        }
    }

    public void ControlMovement(bool enableMove)
    {
        // set if player can move right now
        ovrPlayer.GetComponent<OVRPlayerController>().EnableLinearMovement = enableMove;
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
    }

    private Save CreateSave()
    {
        // save data in the Save script for binary formatting.
        Save save = new Save();
        Vector3 pos = MenuManager.Instance.prevLoc;
        save.playerPositionX = pos.x;
        save.playerPositionY = pos.y;
        save.playerPositionZ = pos.z;
        save.inventory = InventoryManager.Instance.GetInventory();
        save.answers = answers;
        return save;
    }
}
