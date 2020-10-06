using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Character : MonoBehaviour
{
    public float friendshipLevel = 0f; // current friendship level with the player
    public bool missionActive = false; // if the player is currently accomplishing this mission
    public string[] talkingPoints = new string[4]; // things to say when not in mission
    public string[] missionStatement = new string[4]; // mission statements
    public Animator animator; // the characters animator

    // Protcted Vars
    protected List<Character> friends = new List<Character>(); // other characters this one is friendly with
    protected List<Character> enemies = new List<Character>(); // other charaters this one hates
    protected float friendBoost = 0f; // boost for completing a friends mission, or failing/quiting an enemy's mission
    protected float enemyLoss = 0f; // loss for completing a friends mission, or quiting/failing a friend's mission
    protected bool isSpeaking = false; // if character is currently speaking to player
    protected bool inRange = false; // if player is in range to talk to character

    protected virtual void Start()
    {
        //animator = GetComponent<Animator>();
    }

    public void IncreaseFriendship()
    {
        friendshipLevel += friendBoost;
    }

    public void DecreaseFriendship()
    {
        friendshipLevel -= enemyLoss;
    }

    public void ControlSpeaking()
    {
        // if speaking to the character, start character animation, and disable player linear movement or vice versa
        animator.SetBool("isSpeaking",isSpeaking);
        GameManager.Instance.ControlMovement(!isSpeaking);
    }

    protected virtual void Update()
    {
        // if player in range of the character, and not already speaking to them, if press A, we can speak to them
        if(inRange && !isSpeaking)
        {
            if((Input.GetKeyDown(KeyCode.A) || OVRInput.Get(OVRInput.Button.One)))
            {
                isSpeaking =true;
                ControlSpeaking();
            }
        }

        // if speaking to the character and press B, exit dialogue
        if(isSpeaking)
        {
            if((Input.GetKeyDown(KeyCode.B) || OVRInput.Get(OVRInput.Button.Two)))
            {
                isSpeaking = false;
                ControlSpeaking();
            }
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        // ? Could i do this with vector3 distance in update instead of a huge ass hitbox?
        if(other.tag == "lHand" || other.tag == "rHand")
        {
            inRange = true;
        }
    }
    
    protected void OnTriggerExit(Collider other)
    {
        // ? Could i do this with vector3 distance in update instead of a huge ass hitbox?
        if(other.tag == "lHand" || other.tag == "rHand")
        {
            inRange = false;
        }
    }
}
