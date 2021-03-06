using UnityEngine;
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
    //private bool inventoryOpen = false;

    void Start()
    {

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
        GameManager.Instance.ChangeSceneTo(GameManager.Instance.prevScene,GameManager.Instance.playerLoadLocation, GameManager.Instance.playerLoadRotation);
    }
    
    public void Save()
    {
        GameManager.Instance.Save();
    }

    public void Load()
    {
        buttonPanel.SetActive(false);
        loadingText.text = "Loading...";
        GameManager.Instance.Load();
        GameManager.Instance.ChangeSceneTo(0,new Vector3(0f,GameManager.Instance.playerStartY, 0f), new Vector3(0f,0f,0f));

    }

    public void Quit()
    {
        buttonPanel.SetActive(false);
        loadingText.text = "Loading...";
        Application.Quit();
    }
}
