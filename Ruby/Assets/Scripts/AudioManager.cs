using UnityEngine.Audio;
using UnityEngine;
using System;
[System.Serializable]
public class Sound
{
    // Sets up a default Sound object to access
    public string Name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;
    [HideInInspector]
    public AudioSource source;
    public bool loop = false;
    public bool playOnAwake = false;
    public AudioMixerGroup audioGroup;

    public void setSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.loop = loop;
        source.outputAudioMixerGroup = audioGroup;
    }
}

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds; // Creates an array of sounds
    public static AudioManager instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach(Sound s in sounds) // Updates all the sound components onto the selected audio clip
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.audioGroup;
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
