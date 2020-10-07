using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //[Header("P L A Y E R")]
    //[Header("Set In Inspector")]
    //[Header("Set Dynamically")]
    // collectable booleans
    // current location?
    public static Player Instance; // only want one Player
    

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
}
