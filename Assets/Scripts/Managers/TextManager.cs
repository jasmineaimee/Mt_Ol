using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextManager : MonoBehaviour
{
    public static TextManager Instance;
    
    [Header("Set In Inspector")]
    [Header("T E X T  M A N A G E R")]
    public TextMeshPro winText;
    // [Header("Set Dynamically")]

    // Private Vars
    private string[] riddles = new string[10]
    {"Are you ready to begin? \n A: Yes! \t B: Sure?",
    "What is Hephaistos' job on Olympus? \n A: Blacksmithing \t B: Architecture",
    "What represents Aphrodite? \n A: Deer \t B: Myrtles",
    "Who is not one of the Kharities? \n A: Thalia \t B: Eunomia",
    "What is Hera known for being? \n A: Vengeful \t B: Forgiving",
    "What is one thing Athena is the Goddess of? \n A: Young Girls \t B: Warfare",
    "What is Hades God of? \n A: Death \t B: Souls of the Dead",
    "What is Persephone's mother's name? \n A: Demeter \t B: Hera",
    "What animal represents Hermes? \n A: Rabbit \t B: Tortoise",
    "Who is the God of Theives? \n A: Hermes? \t B: Dionysus",
    };
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
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
