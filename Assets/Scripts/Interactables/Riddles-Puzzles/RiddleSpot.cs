using UnityEngine;

public class RiddleSpot : MonoBehaviour
{
    // TODO: Comments
    [Header("R I D D L E  S P O T")]
    [Header("Set In Inspector")]
    public int roomNum;

    [Header("Set Dynamically")]
    public int answer = 0;

    public bool onSpot {
        get;
        private set;
    }
    private int questionNum = 0;

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
            if(roomNum == 5)
            {
                if((Input.GetKeyDown(KeyCode.A) || OVRInput.Get(OVRInput.Touch.One)))
                {
                    answer = 1;
                    GameManager.Instance.answers[questionNum] = 1;
                    questionNum++;
                }
                else if((Input.GetKeyDown(KeyCode.B) || OVRInput.Get(OVRInput.Touch.Two)))
                {
                    answer = 2;
                    GameManager.Instance.answers[questionNum] = 2;
                    questionNum++;
                }

                if(answer != 0)
                {
                    GameManager.Instance.answers[questionNum] = answer;
                    questionNum++;
                    TextManager.Instance.SetQuestionText(roomNum, questionNum);
                    answer = 0;
                }
            }
        }
    }
}
