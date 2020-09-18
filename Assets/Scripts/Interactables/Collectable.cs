using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Collectables {Dirt, Water, Clothing, Grace, Jewellery, Flowers, Wovens, Deceit, Box};
public class Collectable : MonoBehaviour
{
    [Header("C O L L E C T A B L E")]
    [Header("Set in Inspector")]
    public Collectables type;
    public int roomNum;
    public float bodySize; // is for chunity mandolin

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "rHand" || other.gameObject.tag == "lHand")
        {
            InventoryManager.Instance.AddToInventory(type);
            InventoryManager.Instance.SetPodiumHalo(type);
            InventoryManager.Instance.MoveCollectable(this);
            this.GetComponent<Collider>().enabled = false;
        }
    }
}
