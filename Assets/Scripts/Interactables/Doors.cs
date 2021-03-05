using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [Header("Set in Inspector")]
    [Header("D O O R S")]
    public int toRoomNum;
    public int doorNum;
    public float playerLocation;
    [Header("Set Dynamically")]
    public bool isColliding = false;
    // Private Vars
    

    void Start()
    {
        GameManager.Instance.doors.Add(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "rHand" || other.tag == "lHand")
        {
            isColliding = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "rHand" || other.tag == "lHand")
        {
            isColliding = false;
        }
    }
}
