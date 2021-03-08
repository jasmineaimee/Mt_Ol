using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance; // only want one PuzzleManager

    [Header("Set In Inspector")]
    [Header("P U Z Z L E  M A N A G E R")]
    public int AJNFDSFKSFDJB; // just to see in inspector lol
    public const int NUM_PUZZLES = 4; // number of puzzles in the game (regardless of how many times player plays it)
    // ! If above is changes the save file also needs to be changed
    [Header("Set Dynamically")]
    public MazePuzzle mazePuzzle; // reference to maze
    public RiverPuzzle rightRiverSide; // reference to the winning side of the river
    public SimonSaysPuzzle simonSaysPuzzle; // reference to the simon says brain
    public int[,] puzzleStatus; // how many 'points' the player has gotten from the puzzles.
    public bool onLeft = true;
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            mazePuzzle = MazePuzzle.Instance;
            simonSaysPuzzle = SimonSaysPuzzle.Instance;
            puzzleStatus = new int[NUM_PUZZLES, 2];
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        // if they won the river puzzle, give them points
        // if(rightRiverSide.hasWon)
        // {
        //     puzzleStatus[1,0] += 1; // how many times this puzzle has been attempted
        //     puzzleStatus[1,1] += 1; // how many points player has gotten from this puzzle
        // }
    }
}
