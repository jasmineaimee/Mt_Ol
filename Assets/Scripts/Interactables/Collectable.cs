using UnityEngine;

public enum Collectables {Dirt, Water, Clothing, Grace, Jewellery, Flowers, Wovens, Deceit, Box};
public class Collectable : MonoBehaviour
{
    [Header("C O L L E C T A B L E")]
    [Header("Set in Inspector")]
    public Collectables type;
    public int roomNum;

    void Start()
    {
        InventoryManager.Instance.roomCollectable = this.gameObject;
        if(GameManager.Instance.playerInRoom == 0)
        {
            if(GameManager.Instance.hasSeenZeus && !InventoryManager.Instance.isInInventory(type))
            {
                this.gameObject.SetActive(true);
            }
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

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
        this.GetComponent<OVRGrabbable>().allowOffhandGrab = false;
        this.GetComponent<Collider>().enabled = false;
        this.gameObject.SetActive(false);
    }
}
