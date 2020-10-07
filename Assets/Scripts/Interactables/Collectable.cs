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

    void Update()
    {
        if(this.GetComponent<OVRGrabbable>().isGrabbed)
        {
            Invoke("SendToInventory", 2.0f);
        }
    }

    private void SendToInventory()
    {
        InventoryManager.Instance.AddToInventory(type);
        InventoryManager.Instance.SetPodiumHalo(type);
        InventoryManager.Instance.MoveCollectable(this);
        this.GetComponent<OVRGrabbable>().allowOffhandGrab = false;
        this.GetComponent<Collider>().enabled = false;
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if(other.gameObject.tag == "rHand" || other.gameObject.tag == "lHand")
    //     {
            
    //     }
    // }
}
