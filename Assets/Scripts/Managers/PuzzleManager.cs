using UnityEngine;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance; // only want one PuzzleManager

    public const int NUM_PUZZLES = 4; // number of puzzles in the game (regardless of how many times player plays it)
    // ! If above is changed the save file also needs to be changed
    [Header("Set in Inspector")]
    [Header("P U Z Z L E  M A N A G E R")]
    public GameObject boat;
    public GameObject death;
    public GameObject life;
    public GameObject human;
    public RiverPuzzle riverPuzzleLeft;
    public RiverPuzzle riverPuzzleRight;
    private Vector3 characterPosition;
    private Vector3 movePosition;
    private GameObject objectToMove;
    private bool isMoving = false;
    private bool characterMoving = false;
    private bool notStarted = true;
    // Movement speed in units per second.
    public float speed = 1.0F;
    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;
    [Header("Set Dynamically")]
    public int[,] puzzleStatus; // how many 'points' the player has gotten from the puzzles.
    public bool onLeft = true;
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
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
        if(riverPuzzleLeft)
        {
            if(notStarted)
            {
                if(OVRInput.GetUp(OVRInput.Button.One))
                {
                    notStarted = false;
                    ShowOptions();
                    riverPuzzleLeft.isPlaying = true;
                    riverPuzzleRight.isPlaying = true;
                }
            }
            
            // if they won the river puzzle, give them points
            if(!riverPuzzleLeft.hasLost && !riverPuzzleLeft.hasWon)
            {
                if(characterMoving)
                {
                    Debug.Log("Moving Character");
                    float distCovered = (Time.time - startTime) * speed;

                    // Fraction of journey completed equals current distance divided by total distance.
                    float fractionOfJourney = distCovered / journeyLength;

                    // Set our position as a fraction of the distance between the markers.
                    objectToMove.transform.position = Vector3.Lerp(objectToMove.transform.position, characterPosition, fractionOfJourney);
                    if(Vector3.Distance(objectToMove.transform.position, characterPosition) < 0.01)
                    {
                        characterMoving = false;
                        isMoving = true;
                        // Keep a note of the time the movement started.
                        startTime = Time.time;

                        // Calculate the journey length.
                        journeyLength = Vector3.Distance(boat.transform.position, movePosition);
                    }
                }
            }
        }

        if(isMoving)
        {
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            boat.transform.position = Vector3.Lerp(boat.transform.position, movePosition, fractionOfJourney);
            if(Vector3.Distance(boat.transform.position, movePosition) < 0.01)
            {
                isMoving = false;
                movePosition = new Vector3(0,0,0);
                objectToMove = null;
                FinishAnimation();
                ShowOptions();
            }
        }
        if(riverPuzzleRight)
        {
            if(riverPuzzleRight.hasWon)
            {
                puzzleStatus[1,0] += 1; // how many times this puzzle has been attempted
                puzzleStatus[1,1] += 1; // how many points player has gotten from this puzzle
            }
        }
        if(MazePuzzle.Instance != null)
        {
            if(MazePuzzle.Instance.hasWon)
            {
                puzzleStatus[0,0] += 1;
                puzzleStatus[0,1] += (MazePuzzle.Instance.difficulty + 1);
            }
        }
    }


    public void CrossRiver(int position, bool goLeft)
    {
        if(goLeft)
        {
            onLeft = true;
        }
        if(!goLeft)
        {
            onLeft = false;
        }
        
        if(goLeft)
        {
            characterPosition = riverPuzzleRight.characterLocation;
            movePosition = riverPuzzleRight.boatLocation;
        }
        else
        {
            characterPosition = riverPuzzleLeft.characterLocation;
            movePosition = riverPuzzleLeft.boatLocation;
        }
        if(position == 1)
        {
            objectToMove = death;
            characterMoving = true;
            journeyLength = Vector3.Distance(objectToMove.transform.position, characterPosition);
        }
        else if(position == 2)
        {
            objectToMove = life;
            characterMoving = true;
            journeyLength = Vector3.Distance(objectToMove.transform.position, characterPosition);
        }
        else if(position == 3)
        {
            objectToMove = human;
            characterMoving = true;
            journeyLength = Vector3.Distance(objectToMove.transform.position, characterPosition);
        }
        else if(position == 4)
        {
            isMoving = true;
            journeyLength = Vector3.Distance(boat.transform.position, movePosition);
        }

        // Keep a note of the time the movement started.
        startTime = Time.time;
    }
    public void ShowOptions()
    {
        if(!riverPuzzleLeft.hasLost && !riverPuzzleLeft.hasWon)
        {
            riverPuzzleLeft.optionPanel.text = riverPuzzleLeft.GetOptions();
            Debug.Log("Showing Options");
        }
    }
    public void RemoveOptions()
    {
        if(riverPuzzleLeft.hasLost)
        {
            riverPuzzleLeft.optionPanel.text = "Try Again :(";
        }
        if(riverPuzzleLeft.hasWon)
        {
            riverPuzzleLeft.optionPanel.text = "Good Job :)";
        }
    }

    private void FinishAnimation()
    {
        riverPuzzleLeft.isPlaying = true;
        riverPuzzleRight.isPlaying = true;
        riverPuzzleLeft.decisionMade = false;
        riverPuzzleRight.decisionMade = false;
    }
}
