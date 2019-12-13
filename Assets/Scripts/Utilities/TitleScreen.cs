using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [Header("T I T L E  S C R E E N")]
    [Header("Set Dynamically")]
    public static TitleScreen Instance;
    void Start()
    {
        Instance = this;
    }
    public void ButtonHit(string text)
    {
        switch(text)
        {
            case "PlayBtn":
                StartGame();
                break;
            case "LoadBtn":
                LoadGame();
                break;
            case "QuitBtn":
                Quit();
                break;
            default:
                Debug.Log("Text does not match a button. " + text);
                break;
        }
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("loadGame", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("_Scene_0");
    }

    public void LoadGame()
    {
        PlayerPrefs.SetInt("loadGame", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("_Scene_0");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
