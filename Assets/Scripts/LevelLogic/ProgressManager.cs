using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour
{

    private List<string> levels = new List<string>();
    private const string LevelKey = "CurrentLevel";
    // Start is called before the first frame update
    void Start()
    {
        levels.Add("BattleScene");
        levels.Add("BattleScene2");
        levels.Add("BattleScene3");
        levels.Add("BattleScene4");
        levels.Add("BattleScene5");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // do not destroy me when a new scene loads
    }

    public void incrementLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (levels.Contains(currentSceneName))
        {
            int currentLevelIndex = levels.IndexOf(currentSceneName);

            // Key doesn't exist so save current level
            if (!PlayerPrefs.HasKey(LevelKey))
            {

                PlayerPrefs.SetString(LevelKey, levels[currentLevelIndex+1]);
            }

            // Key does exist so check if current level is higher than saved level
            string savedLevel = PlayerPrefs.GetString(LevelKey);
            int savedLevelIndex = levels.IndexOf(savedLevel);
            if (currentLevelIndex + 1 > savedLevelIndex)
            {
                if(currentLevelIndex+1 > levels.Count)
                {
                    return;
                }
                PlayerPrefs.SetString(LevelKey, levels[currentLevelIndex+1]);
            }
        }
    }


}
