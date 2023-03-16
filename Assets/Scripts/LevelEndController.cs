using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndController : MonoBehaviour
{

    //public GameObject mainMenu;

    public void playGame()
    {
        SceneManager.LoadScene("BattleScene");
    }
    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
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
