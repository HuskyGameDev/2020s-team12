using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds; // Creates an array of sounds

    void Awake()
    {
        foreach(Sound s in sounds) // Updates all the sound components onto the selected audio clip
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string SoundName) // Plays the sound clip
    {
        Sound s = Array.Find(sounds, sound => sound.Name == SoundName);
        if(s == null)
        {
            return;
        }
        s.source.Play();
    }
}
