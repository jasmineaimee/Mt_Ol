using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance; // only want one PuzzleManager

    [Header("P U Z Z L E  M A N A G E R")]
    [Header("Set In Inspector")]
    public int AJNFDSFKSFDJB; // just to see in inspector lol
    public const int NUM_PUZZLES = 4; // number of puzzles in the game (regardless of how many times player plays it)
    [Header("Set Dynamically")]
    public MazePuzzle mazePuzzle; // reference to maze
    public RiverPuzzle rightRiverSide; // reference to the winning side of the river
    public SimonSaysPuzzle simonSaysPuzzle; // reference to the simon says brain
    public int[,] puzzleStatus; // how many 'points' the player has gotten from the puzzles.
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            mazePuzzle = MazePuzzle.Instance;
            simonSaysPuzzle = SimonSaysPuzzle.Instance;
            puzzleStatus = new int[NUM_PUZZLES, 2];
        }
    }

    void Update()
    {
        
    }
}
