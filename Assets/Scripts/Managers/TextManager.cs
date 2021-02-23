using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextManager : MonoBehaviour
{
    public static TextManager Instance;
    
    [Header("T E X T  M A N A G E R")]
    [Header("Set In Inspector")]
    public TextMeshPro winText;
    // [Header("Set Dynamically")]

    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    public void SetWinText()
    {
        winText.text = "Winner!";
    }

    public void SetContinueText()
    {
        winText.text = "Keep Looking";
    }

    public void SetLoseText()
    {
        winText.text = "You Lost!";
    }
}
