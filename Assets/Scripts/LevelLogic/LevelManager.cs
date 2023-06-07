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


    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        finalSceneId = 7; // index for final level
        nextLevel = 0;  
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void loadLevel() { // for vs screen, janky but works for now
        currentScene = SceneManager.GetActiveScene().buildIndex;
        nextLevel = currentScene - 6;
        SceneManager.LoadScene(nextLevel);
    }


}
