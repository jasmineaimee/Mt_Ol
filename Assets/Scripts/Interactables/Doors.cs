using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [Header("D O O R S")]
    [Header("Set in Inspector")]
    public float yOpenRot;
    public float yCloseRot;
    
    // Private Vars
    private float timer = 0.0f;
    private bool turning = false;
    private float yRot;

    void Start()
    {
        yRot = yCloseRot;
    }

    void Update()
    {
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
