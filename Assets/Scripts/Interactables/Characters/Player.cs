using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance; // only want one Player

    //[Header("P L A Y E R")]
    //[Header("Set In Inspector")]
    //[Header("Set Dynamically")]
    

    void Awake()
    {
        GameManager.Instance.ovrPlayer = this.gameObject;
        GameManager.Instance.cameraRig = this.transform.Find("OVRCameraRig").gameObject;
        GameManager.Instance.StartAt();
    }
}
