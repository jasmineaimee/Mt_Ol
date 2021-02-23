using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hades/ Underworld Puzzle/Riddle
// You must get all three over the River Styx
// Charon can only take 1 passenger with him across the Styx at a time.
// Without all three, or the watchful eye of Charon keeping them in line;
// If Human is left alone with Death, Death will take them permanently and disappear
// If Human is left alone with Life, Human will take life permanently and disappear
// Solution: Charon Takes H, Returns alone, Takes either L or D, Returns with H, takes other (either L or D), Returns alone, Takes H

// When the player steps on riddle spot they must make choice.
// It does not matter how many trips Charon takes across the river
// Each turn Charon asks Player if he should ferry someone or cross alone.
// Once Charon is crossing the River, the player can move again

// TODO: Text Options based on who's here
public class RiverPuzzle : MonoBehaviour
{
    [Header("R I V E R  P U Z Z L E")]
    [Header("Set In Inspector")]
    public bool isLeft; // if this hitbox is the left(starting hitbox)
    public RiddleSpot riddleSpot; // the corresponding RiddleSpot in the room
    public RiverPuzzle otherSide; // the other hitbox on other side of the river
    [Header("Set Dynamically")]
    public bool hasLost = false; // player lost the riddle
    public bool hasWon = false;
    // Private vars
    private bool deathHere = false; // death character is in this hitbox
    private bool lifeHere = false; // life character is in this hitbox
    private bool humanHere = false; // human character is in this hitbox
    private bool charonHere = false; // charon is in this hitbox
    private bool decisionMade = false; // player has made decision for this turn
    private int decision = 0; // decision player made this turn

    void Start()
    {
        // if this is the hitbox on the left (the start of the river), then all the characters are here.
        if(isLeft)
        {
            deathHere = lifeHere = humanHere = charonHere = true;
            Debug.Log("RiverPuzzle: Set Left bools");
            riddleSpot.answer = -1; // this stops the code in RiddleSpot from going with the keycode/ovrinput
        }
        else
        {
            PuzzleManager.Instance.rightRiverSide = this;
        }
    }

    void Update()
    {   
        // if player is making a Choice for Charon to take
        if(riddleSpot.onSpot && !decisionMade)
        {
            if((PuzzleManager.Instance.onLeft && isLeft) || (!PuzzleManager.Instance.onLeft && !isLeft))
            {
                // Take Death
                if((Input.GetKeyDown(KeyCode.A) || OVRInput.Get(OVRInput.Touch.One)))
                {
                    if(deathHere)
                    {
                        Debug.Log("RiverPuzzle: Chose A");
                        decision = 1;
                        MakeDecision();
                    }
                }
                // Take Life
                if((Input.GetKeyDown(KeyCode.B) || OVRInput.Get(OVRInput.Touch.Two)))
                {
                    if(lifeHere)
                    {
                        Debug.Log("RiverPuzzle: Chose B");
                        decision = 2;
                        MakeDecision();
                    }
                }
                // Take Human
                if((Input.GetKeyDown(KeyCode.X) || OVRInput.Get(OVRInput.Touch.Three)))
                {
                    if(humanHere)
                    {
                        Debug.Log("RiverPuzzle: Chose X");
                        decision = 3;
                        MakeDecision();
                    }
                }
                // Go Alone
                if((Input.GetKeyDown(KeyCode.Y) || OVRInput.Get(OVRInput.Touch.Four)))
                {
                    if(charonHere)
                    {
                        Debug.Log("RiverPuzzle: Chose Y");
                        decision = 4;
                        MakeDecision();
                    }
                }
            }
        }
        // if lose condition is met, after the player has made the choice, do the stuff for losing that was in riddleSpot
        if(decisionMade && ((humanHere && lifeHere && !deathHere && !charonHere) || (humanHere && !lifeHere && deathHere && !charonHere)))
        {
            Debug.Log("RiverPuzzle: OOF");
            hasLost = true;
            int answer = 0;
            GameManager.Instance.answers[riddleSpot.roomNum] = 1;
            GameManager.Instance.ChangeDoorMaterial(riddleSpot.roomNum);
            riddleSpot.gameObject.SetActive(false);
            otherSide.hasLost = true;
            otherSide.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
        // if all the characters are on the other side of the river, player has won
        if(!isLeft && humanHere && lifeHere && deathHere)
        {
            Debug.Log("Won River Puzzle");
            riddleSpot.gameObject.SetActive(false);
            hasWon = true;
            otherSide.hasWon = true;
            otherSide.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        // if we're still playing, and a character has entered the hitbox, adjust location
        if(!hasLost)
        {
            Debug.Log("RiverPuzzle: Trigger Enter with tagged: " + other.tag);
            if(other.tag == "Death")
            {
                deathHere = true;
            }
            if(other.tag == "Life")
            {
                lifeHere = true;
            }
            if(other.tag == "Human")
            {
                humanHere = true;
            }
            if(other.tag == "Charon")
            {
                charonHere = true;
            }
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        // if we're still playing, and a character has exited the hitbox, adjust location
        Debug.Log("RiverPuzzle: Trigger Exit with tagged: " + other.tag);
        if(other.tag == "Death")
        {
            deathHere = false;
        }
        if(other.tag == "Life")
        {
            lifeHere = false;
        }
        if(other.tag == "Human")
        {
            humanHere = false;
        }
        if(other.tag == "Charon")
        {
            charonHere = false;
        }
    }

    private void MakeDecision()
    {
        // player has made the decision, do stuff lol
        // TODO: Characters Cross River
        Debug.Log("RiverPuzzle: Made decision: " + decision);
        if(decision == 0)
        {
            return;
        }
        decisionMade = true;
        if (decision == 1)
        {
            Debug.Log("Chose Life");
        }
        else if (decision == 2)
        {
            Debug.Log("Chose Death");
        }
        else if (decision == 3)
        {
            Debug.Log("Chose Human");
        }
        else if(decision == 4)
        {
            Debug.Log("Crossing Alone");
        }
        Invoke("PlaceHolder", 3.0f);
    }

    private void PlaceHolder()
    {
        // reset decision vars for nect decision
        decision = 0;
        decisionMade = false;
    }
}
