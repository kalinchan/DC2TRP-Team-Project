using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    public Slider musicVolumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
    }

    public void updateMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        AudioManager.instance.musicVolumeChanged();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
