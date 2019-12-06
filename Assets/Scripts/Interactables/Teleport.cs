using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [Header("T E L E P O R T")]
    [Header("Set In Inspector")]
    public string place;

    // Private Vars
    private bool canTeleport;

    void Start()
    {
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
        canTeleport = true;
    }

    void OnTriggerExit(Collider other)
    {
        canTeleport = false;
    }
    void Update()
    {
        if(canTeleport)
        {
            if((Input.GetKeyDown(KeyCode.A) || OVRInput.Get(OVRInput.Button.One)))
            {
                GameManager.Instance.StartTeleport(place);
            }
        }
    }
}
