using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelEndController : MonoBehaviour
{
    public LevelManager levelManager;
    public GameObject nextLevelButton;
    public GameObject levelSelectButton;
    public GameObject mainMenuButton;

    public int currentScene;
    public int nextSceneId;



    //public GameObject mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        nextLevelButton = GameObject.Find("NextLevelButton");
        levelSelectButton = GameObject.Find("LevelSelectButton");
        mainMenuButton = GameObject.Find("MainMenuButton");
        levelManager = GetComponent<LevelManager>();


    }

    // Update is called once per frame
    void Update()
    {

    }

    // play game from start, level 1 --
    public void playGame()
    {
        SceneManager.LoadScene("VS_L1");
    }

    // load main menu --
    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void nextLevel()
    {

        // load next level using level manager --
        currentScene = SceneManager.GetActiveScene().buildIndex;
        nextSceneId = currentScene + 7;
        SceneManager.LoadScene(nextSceneId);
        levelManager.increaseCurrentScene();
    }

    // load level selection screen --
    public void levelSelectScreen()
    {
        SceneManager.LoadScene("Level Selections");
    }


   

}
