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

    private bool hasStarted = false;

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
            if(!hasStarted)
            {
                if(roomNum == 5 && OVRInput.GetUp(OVRInput.Button.One))
                {
                    hasStarted = true;
                    TextManager.Instance.SetQuestionText(roomNum, questionNum);
                }
            }
            else
            {
                if(roomNum == 5 && questionNum < GameManager.Instance.answers.Length)
                {
                    if(!hasAnswered)
                    {
                        if(OVRInput.GetUp(OVRInput.Button.One))
                        {
                            hasAnswered = true;
                            answer = 1;
                        }
                        else if(OVRInput.GetUp(OVRInput.Button.Two))
                        {
                            hasAnswered = true;
                            answer = 2;
                        }
                    }

                    if(answer != 0)
                    {
                        Debug.Log("TEXT ANSWER " + answer + " ::: FOR QUESTION NUMBER " + questionNum);
                        GameManager.Instance.answers[questionNum] = answer;
                        answer = 0;
                        if(questionNum == 9)
                        {
                            TextManager.Instance.SetFinalText();
                            this.gameObject.SetActive(false);
                        }
                        else
                        {
                            TextManager.Instance.SetQuestionText(roomNum, questionNum + 1);
                            Invoke("ResetQuestionVars", 1.0f);
                        }
                        
                    }
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
