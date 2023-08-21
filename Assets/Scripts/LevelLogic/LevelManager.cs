using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public int currentScene;
    public int finalSceneId;
    public bool finalScene;
    public int nextSceneId;
    public int nextLevel;
    public string currentSceneName;


    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        currentSceneName = SceneManager.GetActiveScene().name;
        finalSceneId = 7; // index for final level
        nextLevel = 0;  
        StartCoroutine(DelayedSceneLoad());

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // on LMB click
        {
            Debug.Log("Pressed left-click.");
            if (currentSceneName == "Credits") // skip credits scene
            {
                CreditsOnClick();
            }
            if (currentScene > finalSceneId && currentSceneName != "Credits") // skip VS scene
            {
                VSonClick();
            }
        }

    }

    public void getNextScene()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        nextSceneId = currentScene + 1;

    

    }

    public void increaseCurrentScene()
    {
        currentScene++;
        if (currentScene == finalSceneId)
        {
            finalScene = true;
        }
        else
        {
            finalScene = false;
        }

        // to use for showing EOL - if finalScene is true, show an end of game screen
    }

    //if Screen is VS screen it will move the the next scene after 3 seconds (CB)
    private IEnumerator DelayedSceneLoad()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene > finalSceneId)
        {
            // Wait for 3 seconds before loading the scene
            yield return new WaitForSeconds(3.0f);

            // for vs screen, janky but works for now (CH)
            nextLevel = currentScene - 6;
            SceneManager.LoadScene(nextLevel);
        }
        
    }

    // can skip vs on click
    public void VSonClick()
    {
        loadLevel();
    }

    // can skip credits on click
    public void CreditsOnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void loadLevel() { // for vs screen, janky but works for now
        currentScene = SceneManager.GetActiveScene().buildIndex;
        nextLevel = currentScene - 6;
        SceneManager.LoadScene(nextLevel);
    }


}
