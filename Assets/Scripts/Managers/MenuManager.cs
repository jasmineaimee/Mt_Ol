﻿using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance; // only want one MenuManager

    [Header("Set In Inspector")]
    [Header("M E N U  M A N A G E R")]
    [SerializeField]
    private GameObject buttonPanel;
    [SerializeField]
    private TextMeshProUGUI loadingText;
    // [Header("Set Dynamically")]
    

    // Private Vars

    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void ButtonHit(int mode)
    {
        // which button the player hit in the menu.
        switch(mode)
        {
            case 0:
                Unpause();
                break;
            case 1:
                Save();
                break;
            case 2:
                Load();
                break;
            case -1:
                Quit();
                break;
            default:
                Debug.Log("Invalid Button");
                break;
        }
    }

    public void Unpause()
    {
        // send player back to where they were before.
        buttonPanel.SetActive(false);
        loadingText.text = "Loading...";
        GameManager.Instance.isPaused = false;
        Debug.Log(GameManager.Instance.prevScene);
        GameManager.Instance.ChangeSceneTo(GameManager.Instance.prevScene,GameManager.Instance.playerLoadLocation, GameManager.Instance.playerLoadRotation);
    }
    
    public void Save()
    {
        buttonPanel.SetActive(false);
        GameManager.Instance.Save();
        loadingText.text = "Saved!";
        Invoke("ResetText",2.0f);
    }

    public void Load()
    {
        buttonPanel.SetActive(false);
        loadingText.text = "Loading...";
        GameManager.Instance.Load();
        GameManager.Instance.ChangeSceneTo(0,new Vector3(0f,GameManager.Instance.playerStartY, 0f), Vector3.zero);
    }

    public void Quit()
    {
        buttonPanel.SetActive(false);
        loadingText.text = "Loading...";
        Application.Quit();
    }

    private void ResetText()
    {
        loadingText.text = "";
        buttonPanel.SetActive(true);
    }
}
