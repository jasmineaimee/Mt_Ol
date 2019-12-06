using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("C H A R A C T E R")]
    //[Header("Set In Inspector")]
    [Header("Set Dynamically")]
    public float friendshipLevel;
    public bool missionActive;
    private List<Character> friends;
    private List<Character> enemies;
    private bool isSpeaking;
}
