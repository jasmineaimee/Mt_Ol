using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleSpot : MonoBehaviour
{
    // TODO: Comments
    [Header("R I D D L E  S P O T")]
    [Header("Set In Inspector")]
    public int roomNum;
    public int correctAnswer;

    [Header("Set Dynamically")]
    public int answer;

    public bool onSpot {
        get;
        private set;
    }

    void OnTriggerEnter(Collider other)
    {
        onSpot = true;
    }

    void OnTriggerExit(Collider other)
    {
        onSpot = false;
    }

    void Update()
    {
        
        if(onSpot)
        {
            if(answer == 0)
            {
                if((Input.GetKeyDown(KeyCode.A) || OVRInput.Get(OVRInput.Touch.One)))
                {
                    answer = 1;
                    GameManager.Instance.answers[roomNum] = 1;
                    onSpot = false;
                    this.gameObject.SetActive(false);
                }
                else if((Input.GetKeyDown(KeyCode.B) || OVRInput.Get(OVRInput.Touch.Two)))
                {
                    if(roomNum == 0)
                    {
                        answer = 1;
                    }
                    else
                    {
                        answer = 2;
                    }
                    GameManager.Instance.answers[roomNum] = 2;
                    onSpot = false;
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
