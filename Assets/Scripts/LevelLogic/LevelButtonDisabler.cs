using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonDisabler : MonoBehaviour
{

    public List<Button> buttons;
    private List<string> levels = new List<string>();
    // Start is called before the first frame update

    void Start()
    {
        levels.Add("BattleScene");
        levels.Add("BattleScene2");
        levels.Add("BattleScene3");
        levels.Add("BattleScene4");
        levels.Add("BattleScene5");
        foreach (Button button in buttons)
        { 
            int buttonNumber = int.Parse(button.name.Split(' ')[1]);
            int current = levels.IndexOf(PlayerPrefs.GetString("CurrentLevel"));

            if (buttonNumber <= current+1)
            {
                button.gameObject.SetActive(true);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    
}
