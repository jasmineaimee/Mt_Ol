﻿using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; // only want one InventoryManager

    [Header("I N V E N T O R Y  M A N A G E R")]
    [Header("Set In Inspector")]
    public GameObject[] collectableGOs; // The collectables in collectable order
    public GameObject roomCollectable;
    public GameObject[] podiums; // inventory room collectible podiums, in collectable order
    public GameObject pandorasBox; // the final collectable
    public GameObject pandora; // win character

    [Header("Set Dynamically")]
    public bool boxActive = false; // if box is showing

    //Private Vars
    private List<Collectables> inventory; // player's current collected collectables
    private int tail = 0; // end of the inventory list

    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            // set new inventory
            inventory = new List<Collectables>();
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public Collectables GetLastInventory()
    {
        return inventory[tail];
    }

    public void AddToInventory(Collectables type)
    {
        inventory.Add(type);
        roomCollectable.SetActive(false);
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

    public bool isInInventory(Collectables type)
    {
        return inventory.Contains(type);
    }

    public void SetInventory(List<Collectables> inven)
    {
        // called when loading saved data, update list, and active
        inventory = inven;
        foreach(Collectables i in inventory)
        {
            SetPodiumHalo(i);
            int count = 0;
            bool found  = false;
            GameManager.Instance.numCollectables = inventory.Count;
            while(count < collectableGOs.Length && !found)
            {
                if(collectableGOs[count].GetComponent<Collectable>().type == i)
                {
                    found = true;
                    collectableGOs[count].SetActive(true);
                }
                count++;
            }
        }
    }

    public void SetPodiumHalo(Collectables type)
    {
        // set podium halos when collectable is added to inventory
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

    public void SetWinCondition()
    {
        pandorasBox.SetActive(true);
        pandora.SetActive(true);
        boxActive = true;
        podiums[8].SetActive(true); // green pandora podium
        podiums[9].SetActive(false); // red pandora podium
    }
}
