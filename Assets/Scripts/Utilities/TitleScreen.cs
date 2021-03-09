using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleScreen : MonoBehaviour
{
    public static TitleScreen Instance; // only want one TitleScreen

    [Header("Set in Inspector")]
    [Header("T I T L E  S C R E E N")]
    [SerializeField]
    private GameObject buttonPanel;
    [SerializeField]
    private TextMeshProUGUI loadingText;
    // [Header("Set Dynamically")]
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void StartGame()
    {
        // start new game
        buttonPanel.SetActive(false);
        loadingText.text = "Loading...";
        PlayerPrefs.SetInt("loadGame", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("StartRoom");
    }

    public void LoadGame()
    {
        // load from previous save, if any
        buttonPanel.SetActive(false);
        loadingText.text = "Loading...";
        PlayerPrefs.SetInt("loadGame", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("StartRoom");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
