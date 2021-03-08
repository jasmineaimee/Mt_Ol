﻿using System.Collections;
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
    public Vector3 playerLoadLocation = new Vector3(0f,0f,0f);
    public Vector3 playerLoadRotation = new Vector3(0f,0f,0f);
    // Private Vars
    

    void Start()
    {
        GameManager.Instance.doors.Add(this);
        playerLoadLocation.y = GameManager.Instance.playerStartY;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "rHand" || other.tag == "lHand")
        {
            isColliding = true;
            Debug.Log("isColliding");
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
