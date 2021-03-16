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
    public bool hasAnswered = false;

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
            if(roomNum == 5 && questionNum < GameManager.Instance.answers.Length)
            {
                if(!hasAnswered)
                {
                    if(OVRInput.GetUp(OVRInput.Button.One))
                    {
                        hasAnswered = true;
                        answer = 1;
                        GameManager.Instance.answers[questionNum] = 1;
                        questionNum++;
                    }
                    else if(OVRInput.GetUp(OVRInput.Button.Two))
                    {
                        hasAnswered = true;
                        answer = 2;
                        GameManager.Instance.answers[questionNum] = 2;
                        questionNum++;
                    }
                }

                if(answer != 0)
                {
                    GameManager.Instance.answers[questionNum] = answer;
                    TextManager.Instance.SetQuestionText(roomNum, questionNum);
                    answer = 0;
                    Invoke("ResetQuestionVars", 1.0f);
                }
            }
        }
    }

    private void ResetQuestionVars()
    {
        hasAnswered = false;
        questionNum++;
    }
}
