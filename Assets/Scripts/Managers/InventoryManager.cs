using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("I N V E N T O R Y  M A N A G E R")]
    [Header("Set In Inspector")]
    public GameObject[] collectableGOs;
    public GameObject[] chestLids;
    public GameObject[] podiums;
    public GameObject pandorasBox;
    public GameObject pandora;

    [Header("Set Dynamically")]
    public static InventoryManager Instance;
    public bool boxActive = false;

    //Private Vars
    private List<Collectables> inventory;
    private int tail = 0;

    void Start()
    {
        Instance = this;
        inventory = new List<Collectables>();
    }

    public Collectables GetLastInventory()
    {
        return inventory[tail];
    }

    public void AddToInventory(Collectables type)
    {
        inventory.Add(type);
        tail++;
    }

    public void RemoveFromInventory(Collectables type)
    {
        int i = inventory.IndexOf(type);
        inventory.RemoveAt(i);
        tail--;
    }
    
    public List<Collectables> GetInventory()
    {
        return inventory;
    }

    public void SetInventory(List<Collectables> inven)
    {
        inventory = inven;
        foreach(Collectables i in inventory)
        {
            SetPodiumHalo(i);
            int count = 0;
            bool found  = false;
            while(count < collectableGOs.Length && !found)
            {
                if(collectableGOs[count].GetComponent<Collectable>().type == i)
                {
                    found = true;
                    collectableGOs[count].SetActive(true);
                    MoveCollectable(collectableGOs[count].GetComponent<Collectable>());
                    collectableGOs[count].GetComponent<CapsuleCollider>().enabled = false;
                    int roomNum = collectableGOs[count].GetComponent<Collectable>().roomNum;
                    TextManager.Instance.SetResultText(roomNum, GameManager.Instance.answers[roomNum], GameManager.Instance.riddleSpots[roomNum].GetComponent<RiddleSpot>().correctAnswer);
                    GameManager.Instance.ChangeDoorMaterial(roomNum);
                    GameManager.Instance.riddleSpots[roomNum].SetActive(false);
                }
                count++;
            }
            
        }
    }

    public void SetLidHalo(int num)
    {
        Component lidHalo = chestLids[num].GetComponent("Halo");
        lidHalo.GetType().GetProperty("enabled").SetValue(lidHalo, true, null);
    }

    public void SetPodiumHalo(Collectables type)
    {
        switch(type)
        {
            case Collectables.Dirt:
                Component dirtHalo = podiums[0].GetComponent("Halo");
                dirtHalo.GetType().GetProperty("enabled").SetValue(dirtHalo, true, null);
                break;
            case Collectables.Water:
                Component waterHalo = podiums[1].GetComponent("Halo");
                waterHalo.GetType().GetProperty("enabled").SetValue(waterHalo, true, null);
                break;
            case Collectables.Clothing:
                Component clothingHalo = podiums[2].GetComponent("Halo");
                clothingHalo.GetType().GetProperty("enabled").SetValue(clothingHalo, true, null);
                break;
            case Collectables.Grace:
                Component graceHalo = podiums[3].GetComponent("Halo");
                graceHalo.GetType().GetProperty("enabled").SetValue(graceHalo, true, null);
                break;
            case Collectables.Jewellery:
                Component jewelleryHalo = podiums[4].GetComponent("Halo");
                jewelleryHalo.GetType().GetProperty("enabled").SetValue(jewelleryHalo, true, null);
                break;
            case Collectables.Flowers:
                Component flowersHalo = podiums[5].GetComponent("Halo");
                flowersHalo.GetType().GetProperty("enabled").SetValue(flowersHalo, true, null);
                break;
            case Collectables.Wovens:
                Component wovensHalo = podiums[6].GetComponent("Halo");
                wovensHalo.GetType().GetProperty("enabled").SetValue(wovensHalo, true, null);
                break;
            case Collectables.Deceit:
                Component deceitHalo = podiums[7].GetComponent("Halo");
                deceitHalo.GetType().GetProperty("enabled").SetValue(deceitHalo, true, null);
                break;
            default:
                Debug.Log("Collectable type somehow out of bounds?");
                break;    
        }
    }

    public Vector3 GetPodiumLocation(Collectables type)
    {
        switch(type)
        {
            case Collectables.Dirt:
                return podiums[0].transform.position;
            case Collectables.Water:
                return podiums[1].transform.position;
            case Collectables.Clothing:
                return podiums[2].transform.position;
            case Collectables.Grace:
                return podiums[3].transform.position;
            case Collectables.Jewellery:
                return podiums[4].transform.position;
            case Collectables.Flowers:
                return podiums[5].transform.position;
            case Collectables.Wovens:
                return podiums[6].transform.position;
            case Collectables.Deceit:
                return podiums[7].transform.position;
            default:
                Debug.Log("No podium to grab or out of bounds. " + type);
                return Vector3.zero;
        }
    }

    public void SetGrabbable(int num)
    {
        SetLidHalo(num);
        chestLids[num].GetComponent<OVRGrabbable>().allowOffhandGrab = true;
    }

    public void SetCollectableActive(int num)
    {
        switch(num)
        {
            case 0:
                collectableGOs[0].SetActive(true);
                break;
            case 2:
                collectableGOs[3].SetActive(true);
                break;
            case 3:
                collectableGOs[4].SetActive(true);
                break;
            case 4:
                collectableGOs[1].SetActive(true);
                break;
            case 5:
                collectableGOs[2].SetActive(true);
                break;
            case 6:
                collectableGOs[6].SetActive(true);
                break;
            case 7:
                collectableGOs[5].SetActive(true);
                break;
            case 8:
                collectableGOs[7].SetActive(true);
                break;
            default:
                Debug.Log("No collectable to activate or out of bounds: " + num);
                break;
        }
    }

    public void MoveCollectable(Collectable collectable)
    {
        Vector3 location = GetPodiumLocation(collectable.type);
        collectable.gameObject.transform.position = new Vector3(location.x, location.y + 1.0f, location.z);
    }

    void Update()
    {
        if(inventory.Count > 6 && System.Array.IndexOf(GameManager.Instance.answers, 0) == -1)
        {
            pandorasBox.SetActive(true);
            pandora.SetActive(true);
            boxActive = true;
            TextManager.Instance.SetWinText();
            podiums[8].SetActive(true); // green pandora podium
            podiums[9].SetActive(false); // red pandora podium
        }
        else if(System.Array.IndexOf(GameManager.Instance.answers, 0) == -1 && inventory.Count <= 6)
        {
            TextManager.Instance.SetLoseText();
        }
        else
        {
            TextManager.Instance.SetContinueText();
        }
    }
}
