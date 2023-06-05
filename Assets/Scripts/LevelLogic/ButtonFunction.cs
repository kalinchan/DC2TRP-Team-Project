using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunction : MonoBehaviour{



// This Script allows the button on the level selection screen to be active and load the correct scene

// For the first level this script was made as the name of the scene does not have number so i could not use a number varaible to
//seperate and assign level

//so I created a seperate scrscript for the first button.

// Other script is called "ButtonFunctionPT2"


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenScene(){
        SceneManager.LoadScene("BattleScene");
    }

}
