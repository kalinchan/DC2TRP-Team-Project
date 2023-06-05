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


    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        finalSceneId = 7; // index for final level
        
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


}
