using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance; // only want one MenuManager


    [Header("M E N U  M A N A G E R")]
    //[Header("Set In Inspector")]
    [Header("Set Dynamically")]
    public bool isPaused = false; // is the player paused right now (in the menu area)
    public Vector3 prevLoc = new Vector3(0f,0f,0f); // where was the player when they hit menu button?

    // Private Vars
    //private bool inventoryOpen = false;

    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            // set location to teleport player into menu
            GameManager.Instance.menuTeleport = new Vector3(139f,GameManager.Instance.playerStartY, 0f);
        }
    }

    public void ButtonHit(string text)
    {
        // which button the player hit in the menu.
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
        // send player back to where they were before.
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
        // if the player hit the menu button, teleport them to menu and pause the game.
        // if((Input.GetKeyDown(KeyCode.Escape) || OVRInput.Get(OVRInput.Button.Start)))
        // {
            // prevLoc = GameManager.Instance.ovrPlayer.transform.position;
            // isPaused = true;
            // GameManager.Instance.StartTeleport("Menu");
        //}
    }
}
