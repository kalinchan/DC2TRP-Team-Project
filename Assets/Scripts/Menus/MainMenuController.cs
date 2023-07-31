using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject optionsMenu;
    private LevelManager levelManager;
    public GameObject continueButton;
    private const string LevelKey = "CurrentLevel";
    private ProgressManager progressManager;

    private Dictionary<string, string> versus = new Dictionary<string, string>();


    public void playGame()
    {
        SceneManager.LoadScene("VS_L1");
        PlayerPrefs.DeleteKey("CurrentLevel");
        PlayerPrefs.DeleteKey("Deck");
        PlayerPrefs.DeleteKey("SpecialCards");
        AudioManager.instance.PlaySound("Button Click");
        progressManager.ResetFirstPlayDictionary();
    }

    public void tutorial()
    {
        SceneManager.LoadScene("Tutorial");
        AudioManager.instance.PlaySound("Button Click");
    }

    public void options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        AudioManager.instance.PlaySound("Button Click");
    }

    public void back()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        AudioManager.instance.PlaySound("Button Click");
    }
    public void continueGame()
    {
        string savedLevel = PlayerPrefs.GetString(LevelKey);
        Debug.Log(versus[savedLevel]);
        string scene;
        versus.TryGetValue(savedLevel, out scene);
        SceneManager.LoadScene(scene);
        AudioManager.instance.PlaySound("Button Click");
    }


    public void exitGame()
    {
        AudioManager.instance.PlaySound("Button Click");
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(LevelKey))
        {
            continueButton.SetActive(true);
        }

        versus.Add("BattleScene", "VS_L1");
        versus.Add("BattleScene2", "VS_L2");
        versus.Add("BattleScene3", "VS_L3");
        versus.Add("BattleScene4", "VS_L4");
        versus.Add("BattleScene5", "VS_L5");

        progressManager = GameObject.Find("Progress").GetComponent<ProgressManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
