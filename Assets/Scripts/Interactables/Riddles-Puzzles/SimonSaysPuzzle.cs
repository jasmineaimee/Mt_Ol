using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSaysPuzzle : MonoBehaviour
{
    public static SimonSaysPuzzle Instance;
    
    [Header("Set In Inspector")]
    [Header("S I M O N  S A Y S  P U Z Z L E")]
    public bool isSounds; // the sound version of simon says
    public GameObject[] buttons; // the buttons the player can hit
    public RiddleSpot riddleSpot; // the corresponding RiddleSpot in the room
    [Header("Set Dynamically")]
    public int numberCorrect = 0; // how far the player played to.
    public bool hasLost = false; // if the player has lost
    public bool hasWon = false; // if the player has won
    // Private vars
    private List<int> sequence; // the current sequence of colours/sounds
    private int currentSeqPos = 0; // currnt position player is in the sequence
    private bool isPlaying = false; // if the sequence is currently playing
    private const int WIN_LEVEL = 5; // sequence level player needs to win
    private bool gameStarted = false;

    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            // set new sequence list, and fill start with a colour.
            sequence = new List<int>();
            sequence.Add(Random.Range(1,9));
        }
    }

    void Update()
    {
        if(numberCorrect != WIN_LEVEL) // if the player has not reached winning number of levels
        {
            // if the sequence is not currently playing and the player presses A while on the riddlespot, show them the sequence again, or start game if not already started.
            if(!isPlaying && riddleSpot.onSpot)
            {
                if((Input.GetKeyDown(KeyCode.A) || OVRInput.Get(OVRInput.Touch.One)))
                {
                    Debug.Log("SHOWING SEQUENCE");
                    showSequence();
                    gameStarted = true;
                    hasLost = false;
                }
            }
        }
        else
        {
            // TODO: All the other win stuff from RiddleSpot lol
            Debug.Log("Won SIMON SAYS");
            riddleSpot.gameObject.SetActive(false);
            GameManager.Instance.roomCollectable.SetActive(true);
        }
    }

    private void showSequence()
    {
        // show first halo in two seconds (1, plus the IEnumerator 1)
        isPlaying = true;
        Invoke("ShowHalo", 1.0f);
    }

    private IEnumerator ShowHalo(int colour = -1, int counter = 0)
    {
        Debug.Log("Colour: " + colour + " Count: " + counter);
        yield return new WaitForSeconds(1.0f);
        // if this was just invoked, we know it's the start of the sequence.
        if(colour == -1)
        {
            colour = sequence[0];
        }
        // if this is the sounds one, play the correct sound
        if(isSounds)
        {
            SoundManager.Instance.PlayRiddleSound(colour);
        }
        else // else show the hao for .5 seconds
        {
            Component halo = buttons[colour].GetComponent("Halo");
            halo.GetType().GetProperty("enabled").SetValue(halo, true, null);
            HideHalo(colour);
        }
        // if not at the end of the sequence, recursion call
        if(counter < sequence.Count - 1)
        {
            ShowHalo(sequence[counter+1], counter+1);
        }
        else // at end of sequence, it's not playing anymore
        {
            isPlaying = false;
        }
    }

    private IEnumerator HideHalo(int colour)
    {
        // turn of halo after .5 seconds
        yield return new WaitForSeconds(0.5f);
        if(!isSounds)
        {
            Component halo = buttons[colour].GetComponent("Halo");
            halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
        }
    }

    public void UpdateAnswers(int answer)
    {
        Debug.Log(answer);
        // if the player has started the game
        if(gameStarted) {
            // if this hit is still in the correct sequence order
            if(answer == sequence[currentSeqPos])
            {
                // if player is at end of sequence, play win sound, reset position, add to level score, and add a new value to the sequence
                if(currentSeqPos == sequence.Count - 1)
                {
                    // play win sound, resest sequence position, 
                    SoundManager.Instance.PlayOneShot(SoundManager.Instance.correctClip);
                    Debug.Log("GOT TO END OF SEQUENCE");
                    currentSeqPos = 0;
                    numberCorrect++;
                    sequence.Add(Random.Range(1,9));
                    return;
                }
                // incremenet sequence position 
                currentSeqPos++;
            }
            else // player did not play correct sequence
            {
                // player lost, reset sequence, and sequence position, and score
                Debug.Log("Player Lost. Correct Sequence: " + sequence);
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.incorrectClip);
                hasLost = true;
                currentSeqPos = 0;
                numberCorrect = 0;
                sequence.Clear();
                sequence.Add(Random.Range(1,9));
                gameStarted = false;
            }
        }
    }
}
