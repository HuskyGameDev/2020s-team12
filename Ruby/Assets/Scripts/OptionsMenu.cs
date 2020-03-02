using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audiomixer;
    public Slider volumeSlider;
    public Toggle Fullscreen;

    void Start()
    {

        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f); // Sets the slider to the correct position

        audiomixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume")); // Sets the audio level that was preferred
        
        Fullscreen.isOn = ((PlayerPrefs.GetInt("Fullscreen") != 0)); // Sets preference for fullscreen
      
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("Volume", volume); // Updates preferences

        audiomixer.SetFloat("Volume", Mathf.Log10 (volume) * 20); // Updates audio level
    }

    public void SetFullscreen(bool isFullscreen)
    {
        PlayerPrefs.SetInt("Fullscreen", (isFullscreen ? 1 : 0)); // Updates preferences

        Screen.fullScreen = isFullscreen; // Updates fullscreen
        
    }
}
