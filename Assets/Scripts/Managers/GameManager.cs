using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("G A M E  M A N A G E R")]
    [Header("Set In Inspector")]
    public GameObject ovrPlayer;
    public GameObject cameraRig;
    public GameObject[] riddleSpots;
    public int[] answers = new int[10] {0,0,0,0,0,0,0,0,0,0};
    public GameObject[] doors;
    public GameObject pandoraDoor;
    public Material wood;


    [Header("Set Dynamically")]
    public static GameManager Instance;
    public bool isPlayerActive = true;
    public bool recentre = false;
    public Vector3 teleportLocation;
    public Vector3 underTeleport;
    public Vector3 hadesTeleport;
    public Vector3 menuTeleport;
    public float playerStartY;
    
    
    // Private Vars
    private Vector3 loadPos = Vector3.zero;
    private bool loading = false;

    void Start()
    {
        Instance = this;
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
        Save save = CreateSave();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Game Saved");
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            loading = true;
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

        Invoke("Teleport", .5f);
    }


    public void Teleport()
    {
        ovrPlayer.transform.position = teleportLocation;
        ovrPlayer.transform.rotation = Quaternion.identity;
        isPlayerActive = true;
        recentre = true;
    }

    public void ChangeDoorMaterial(int num)
    {
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
    
     void Update()
    {
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
