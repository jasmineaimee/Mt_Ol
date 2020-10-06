using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public float playerPositionX = 0f; // player's x position before menu
    public float playerPositionY = 0f; // player's y position before menu
    public float playerPositionZ = 0f; // player's z position before menu

    public List<Collectables> inventory = new List<Collectables>(); // their collectables if any
    public int[] answers = new int[10]; // the answers they have given thus far.
}
