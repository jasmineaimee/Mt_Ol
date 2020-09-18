using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("M E N U  M A N A G E R")]
    //[Header("Set In Inspector")]
    [Header("Set Dynamically")]
    public static MenuManager Instance;
    public bool isPaused = false;
    public Vector3 prevLoc = new Vector3(0f,0f,0f);

    // Private Vars
    //private bool inventoryOpen = false;

    void Start()
    {
        Instance = this;
        GameManager.Instance.menuTeleport = new Vector3(139f,GameManager.Instance.playerStartY, 0f);
    }

    public void ButtonHit(string text)
    {
        switch(text)
        {
            case "ResumeBtn":
                Unpause();
                break;
            case "SaveBtn":
                Save();
                break;
            case "LoadBtn":
                Load();
                break;
            case "QuitBtn":
                Quit();
                break;
            default:
                Debug.Log("Text does not match a button. " + text);
                break;
        }
    }

    public void Unpause()
    {
        isPaused = false;
        GameManager.Instance.StartTeleport("Back");
    }
    
    public void Save()
    {
        GameManager.Instance.Save();
    }

    public void Load()
    {
        GameManager.Instance.Load();
    }

    public void Quit()
    {
        Application.Quit();
    }

    void Update()
    {
        if((Input.GetKeyDown(KeyCode.Escape) || OVRInput.Get(OVRInput.Button.Start)))
        {
            prevLoc = GameManager.Instance.ovrPlayer.transform.position;
            isPaused = true;
            GameManager.Instance.StartTeleport("Menu");
        }
    }
}
