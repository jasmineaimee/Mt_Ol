using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenBtnHit : MonoBehaviour
{
    [Header("T I T L E S C R E E N B T N H I T")]
    [Header("Set In Inspector")]
    public string thisName;
    void OnTriggerEnter(Collider other)
    {
        TitleScreen.Instance.ButtonHit(thisName);
    }
}
