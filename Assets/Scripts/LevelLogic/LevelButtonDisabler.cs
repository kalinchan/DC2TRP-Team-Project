using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonDisabler : MonoBehaviour
{

    public List<Button> buttons;
    // Start is called before the first frame update
    void Start()
    {
        int maxLevel = PlayerPrefs.GetInt("Player_Max_Level", 1);
        foreach(Button button in buttons)
        { 
            int buttonNumber = int.Parse(button.name.Split(' ')[1]);
            if (buttonNumber > maxLevel-1) //-1 because level 1 is scene index 3 and you want to be able to access the next level
            {
                button.interactable = false;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
