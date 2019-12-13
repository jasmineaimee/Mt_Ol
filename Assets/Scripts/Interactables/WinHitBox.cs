using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinHitBox : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(InventoryManager.Instance.boxActive)
        {
            Doors door = GameManager.Instance.pandoraDoor.GetComponent<Doors>();
            if(door.yRot == door.yOpenRot)
            {
                door.yRot = door.yCloseRot;
                door.turning = true;
                Invoke("UnsetDoorTrigger", 1.5f);
            }
        }
    }

    private void UnsetDoorTrigger()
    {
        GameManager.Instance.pandoraDoor.GetComponent<Collider>().isTrigger = false;

    }
}
