using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBtnHit : MonoBehaviour
{
    [Header("M E N U B T N H I T")]
    [Header("Set In Inspector")]
    public string thisName; // which button this is
    void OnTriggerEnter(Collider other)
    {
        MenuManager.Instance.ButtonHit(thisName);
    }
}
