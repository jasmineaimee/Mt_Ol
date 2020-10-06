using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [Header("T E L E P O R T")]
    [Header("Set In Inspector")]
    public string place; // is this underworld or hades room

    // Private Vars
    private bool canTeleport; // is player on teleporation spot?

    void Start()
    {
        // set GameManager vars for teleportation locations
        if(place == "Hades")
        {
            GameManager.Instance.hadesTeleport = transform.position;
        }
        else if (place == "Underworld")
        {
            GameManager.Instance.underTeleport = transform.position;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // if player is on the telelport pad, they can be teleported
        canTeleport = true;
    }

    void OnTriggerExit(Collider other)
    {
        canTeleport = false;
    }
    void Update()
    {
        // if player presses A on teleportation pad, player gets telported
        if(canTeleport)
        {
            if((Input.GetKeyDown(KeyCode.A) || OVRInput.Get(OVRInput.Button.One)))
            {
                GameManager.Instance.StartTeleport(place);
            }
        }
    }
}
