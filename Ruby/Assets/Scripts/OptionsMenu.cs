using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audiomixer;
    public Slider volumeSlider;
    public Toggle fullScreen;

    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f); // Sets the volume slider at the correct location
        audiomixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume")); // Sets the audio mixer to the correct volume

        Screen.fullScreen = (PlayerPrefs.GetInt("Fullscreen") != 0); // Checks to see if it's true and sets it to true
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("Volume", volume); // Sets the player pref to the right audio
        audiomixer.SetFloat("Volume", Mathf.Log10 (volume) * 20); // Sets float of audio mixer
    }

    public void SetFullscreen(bool isFullscreen)
    {
        PlayerPrefs.SetInt("Fullscreen", (isFullscreen ? 1 : 0)); // Sets fullscreen boolean value in player preferences
        Screen.fullScreen = isFullscreen; // Sets fullscreen
    }
}
