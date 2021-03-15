using UnityEngine;
using TMPro;
abstract public class Character : MonoBehaviour
{
    [Header("Set in Inspector")]
    [Header("C H A R A C T E R")]
    public TextMeshProUGUI speechText; // if in range and a is pressed this shows character dialogue
    public GameObject riddleSpot; // if this is needed for the character it's here
    public GameObject teleport; // if this is needed for the character it's here

    [Header("Set Dynamically")]
    public bool missionActive = false; // if the player is currently accomplishing this mission
    public string[] talkingPoints = new string[4]; // things to say when not in mission
    public string missionStatement = ""; // mission statements

    // Protcted Vars
    protected bool isSpeaking = false; // if character is currently speaking to player
    protected bool inRange = false; // if player is in range to talk to character

    protected virtual void Start()
    {
    }

    protected virtual void ControlSpeaking()
    {
        if(!missionActive && GameManager.Instance.hasSeenZeus)
        {
            speechText.text = missionStatement;
            missionActive = true;
        }
        else
        {
            speechText.text = talkingPoints[Random.Range(0,4)];
        }
    }

    protected virtual void Update()
    {
        // if player in range of the character, and not already speaking to them, if press A, we can speak to them
        if(inRange && !isSpeaking)
        {
            isSpeaking =true;
            ControlSpeaking();
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(other.tag == "lHand" || other.tag == "rHand")
        {
            inRange = true;
        }
    }
    
    protected void OnTriggerExit(Collider other)
    {
        if(other.tag == "lHand" || other.tag == "rHand")
        {
            inRange = false;
        }
    }
}
