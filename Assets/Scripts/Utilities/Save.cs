using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public float playerPositionX = 0f;
    public float playerPositionY = 0f;
    public float playerPositionZ = 0f;

    public List<Collectables> inventory = new List<Collectables>();
    public int[] answers = new int[10];
}
