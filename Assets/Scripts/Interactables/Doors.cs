using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [Header("D O O R S")]
    [Header("Set in Inspector")]
    public float yOpenRot; // y degree when door is open
    public float yCloseRot; // y degree when door is closed

    [Header("Set Dynamically")]
    public float yRot; // degree to turn door to
    public bool turning = false; // is door currently turning
    
    // Private Vars
    private float timer = 0.0f; // time for door to open/close
    

    void Start()
    {
        // all doors start closed
        yRot = yCloseRot;
    }

    void Update()
    {
        // if it's turning rotate the door, until it's completely open
        if(turning)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0.0f, yRot, 0.0f), timer);
            timer += Time.deltaTime;
            if(timer > 1)
            {
                timer = 0.0f;
                turning = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // if not already turning, if player hits door, open/close door.
        if(!turning)
        {
            if(other.gameObject.tag == "rHand" || other.gameObject.tag == "lHand")
            {
                if(yRot == yOpenRot)
                {
                    yRot = yCloseRot;
                }
                else
                {
                    yRot = yOpenRot;
                }
                turning = true;
            }
        }
    }
}
