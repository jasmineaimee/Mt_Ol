using UnityEngine;
using TMPro;
abstract public class Character : MonoBehaviour
{
    [Header("Set in Inspector")]
    [Header("C H A R A C T E R")]
    // public float friendshipLevel = 0f; // current friendship level with the player
    public TextMeshProUGUI aText;
    public TextMeshProUGUI speechText;

    [Header("Set Dynamically")]
    public bool missionActive = false; // if the player is currently accomplishing this mission
    public string[] talkingPoints = new string[4]; // things to say when not in mission
    public string[] missionStatement = new string[4]; // mission statements

    // Protcted Vars
    // protected List<Character> friends = new List<Character>(); // other characters this one is friendly with
    // protected List<Character> enemies = new List<Character>(); // other charaters this one hates
    // protected float friendBoost = 0f; // boost for completing a friends mission, or failing/quiting an enemy's mission
    // protected float enemyLoss = 0f; // loss for completing a friends mission, or quiting/failing a friend's mission
    protected bool isSpeaking = false; // if character is currently speaking to player
    protected bool inRange = false; // if player is in range to talk to character

    protected virtual void Start()
    {
    }

    // public void IncreaseFriendship()
    // {
    //     friendshipLevel += friendBoost;
    // }

    // public void DecreaseFriendship()
    // {
    //     friendshipLevel -= enemyLoss;
    // }

    public virtual void ControlSpeaking()
    {

    }

    protected virtual void Update()
    {
        // if player in range of the character, and not already speaking to them, if press A, we can speak to them
        if(inRange && !isSpeaking)
        {
            if((Input.GetKeyDown(KeyCode.A) || OVRInput.Get(OVRInput.Touch.One)))
            {
                isSpeaking =true;
                ControlSpeaking();
            }
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
