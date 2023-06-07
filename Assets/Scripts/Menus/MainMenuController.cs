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

    public void playGame()
    {
        SceneManager.LoadScene("VS_L1");
        AudioManager.instance.PlaySound("Button Click");
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

    public void exitGame()
    {
        AudioManager.instance.PlaySound("Button Click");
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
