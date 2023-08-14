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
    private GradeManager gradeManager;
    public GameObject progress;
    private Dictionary<string, string> versus = new Dictionary<string, string>();
    public GameObject panel;
    public List<GameObject> buttonToHide;


    public void playGame()
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            panel.SetActive(true);
            foreach(GameObject gameObject in buttonToHide)
            {
                gameObject.SetActive(false);
            }
            return;
        }
        SceneManager.LoadScene("VS_L1");
        AudioManager.instance.PlaySound("Button Click");
    }

    public void warningContinue()
    {
        SceneManager.LoadScene("VS_L1");
        PlayerPrefs.DeleteKey("CurrentLevel");
        PlayerPrefs.DeleteKey("Deck");
        PlayerPrefs.DeleteKey("SpecialCards");
        PlayerPrefs.DeleteKey("GradeDict");
        AudioManager.instance.PlaySound("Button Click");
        progressManager.ResetFirstPlayDictionary();
        gradeManager.ResetGrades();
        gradeManager.ResetMoves();
    }

    public void warningBack()
    {
        panel.SetActive(false);
        foreach (GameObject gameObject in buttonToHide)
        {
            gameObject.SetActive(true);
        }
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
        gradeManager.LoadGradesFromPlayerPrefs();
    }


    public void exitGame()
    {
        AudioManager.instance.PlaySound("Button Click");
        Application.Quit();
    }

    public void LoadCredits()
    {
        AudioManager.instance.PlaySound("Button Click");
        AudioManager.instance.StopMusic();
        SceneManager.LoadScene("Credits");
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(LevelKey))
        {
            continueButton.SetActive(true);
            
        }

        AudioManager.instance.PlayMusic();

        versus.Add("BattleScene", "VS_L1");
        versus.Add("BattleScene2", "VS_L2");
        versus.Add("BattleScene3", "VS_L3");
        versus.Add("BattleScene4", "VS_L4");
        versus.Add("BattleScene5", "VS_L5");

        progress = GameObject.Find("Progress");
        progressManager = progress.GetComponent<ProgressManager>();
        gradeManager = progress.GetComponent<GradeManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
