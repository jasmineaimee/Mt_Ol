using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleSpot : MonoBehaviour
{
    [Header("R I D D L E  S P O T")]
    [Header("Set In Inspector")]
    public int roomNum;
    public int correctAnswer;

    [Header("Set Dynamically")]
    public int answer;

    //Private Vars
    private bool onSpot = false;

    void OnTriggerEnter(Collider other)
    {
        onSpot = true;
        TextManager.Instance.SetText(roomNum);
    }

    void OnTriggerExit(Collider other)
    {
        onSpot = false;
        if(answer == 0)
        {
            TextManager.Instance.ResetText(roomNum);
        }
    }

    void Update()
    {
        
        if(onSpot)
        {
            if(answer == 0)
            {
                if((Input.GetKeyDown(KeyCode.A) || OVRInput.Get(OVRInput.Button.One)))
                {
                    answer = 1;
                    GameManager.Instance.answers[roomNum] = 1;
                    onSpot = false;
                    TextManager.Instance.SetResultText(roomNum,answer,correctAnswer);
                    GameManager.Instance.ChangeDoorMaterial(roomNum);
                    this.gameObject.SetActive(false);
                }
                else if((Input.GetKeyDown(KeyCode.B) || OVRInput.Get(OVRInput.Button.Two)))
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
                    TextManager.Instance.SetResultText(roomNum,answer,correctAnswer);
                    GameManager.Instance.ChangeDoorMaterial(roomNum);
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
