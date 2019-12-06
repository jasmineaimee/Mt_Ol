﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// start = a/b, hephaistos = a, aphrodite = a, kharities = b, inventory/pandora, hera = a, athena = b, hades = b, hermes = b, zeus = a, underworld = 
public class TextManager : MonoBehaviour
{
    [Header("T E X T  M A N A G E R")]
    [Header("Set In Inspector")]
    public TextMeshPro[] riddleText;
    [Header("Set Dynamically")]
    public static TextManager Instance;

    // Private Vars
    private string ready = "Ready?";
    private string correct = "Correct!";
    private string incorrect = "Incorrect...";
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
        Instance = this;
    }

    public void SetText(int num)
    {
        riddleText[num].text = riddles[num];
    }

    public void ResetText(int num)
    {
        riddleText[num].text = ready;
    }

    public void SetResultText(int num, int answer, int correctAnswer)
    {
        Debug.Log(num + " " + (answer == correctAnswer));

        if(answer == correctAnswer)
        {
            riddleText[num].text = correct;
            InventoryManager.Instance.SetGrabbable(num);
            InventoryManager.Instance.SetCollectableActive(num);
        }
        else
        {
            riddleText[num].text = incorrect;
        }
    }
}
