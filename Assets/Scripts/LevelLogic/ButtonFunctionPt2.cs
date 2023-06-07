using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctionPt2 : MonoBehaviour {

    //Unlike The script "ButtonFunction" by using the varaialbe level i can easily change scene as level 2 to level 5 scnene ends
    //with numbers.

    public int Level;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenScenes() {
        Level = Level;
        SceneManager.LoadScene("VS_L" + Level);
    }
}
