using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSaysPuzzle : MonoBehaviour
{
    [Header("S I M O N  S A Y S  P U Z Z L E")]
    [Header("Set In Inspector")]
    public bool isSounds; // the sound version of simon says
    [Header("Set Dynamically")]
    public int numberCorrect = 0; // how far the player played to.
    // Private vars
    private List<int> sequence;



    void Start()
    {
        // set new sequence list, and fill start with a colour.
        sequence = new List<int>();
        sequence.Add(Random.Range(1,8));
    }

    void Update()
    {
        
    }
}
