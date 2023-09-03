using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{

    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;
    public GameObject mainMenuButton;
    public GameObject levelSelectButton;


    // Start is called before the first frame update
    void Start()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);
        mainMenuButton = GameObject.Find("MainMenuButton");
        levelSelectButton= GameObject.Find("LevelSelectButton");
    }

    public void updateMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        AudioManager.instance.musicVolumeChanged();
    }

    public void updateEffectsVolume()
    {
        PlayerPrefs.SetFloat("EffectsVolume", effectsVolumeSlider.value);
        AudioManager.instance.effectVolumeChanged();
    }

    // load main menu --
    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void levelSelectScreen()
    {
        SceneManager.LoadScene("Level Selections");
    }

}
