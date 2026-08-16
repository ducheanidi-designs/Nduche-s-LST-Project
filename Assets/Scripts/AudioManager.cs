using UnityEngine;
using UnityEngine.Audio;
using System;

// This makes the custom Sound class visible in the Unity Inspector
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(.1f, 3f)]
    public float pitch = 1f;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}

public class AudioManager : MonoBehaviour
{
    // Singleton instance so you can call it from anywhere
    public static AudioManager instance;

    public Sound[] sounds;

    void Awake()
    {
        // Singleton pattern to ensure only one AudioManager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Keeps the audio playing smoothly even when you load new levels
        DontDestroyOnLoad(gameObject);

        // Generate an AudioSource for every sound in the array
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        // Start the background music immediately
        Play("BgSOUND");
    }

    // Standard play method (best for Music, Running, and looping sounds)
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        
        // Don't play if it's already playing (prevents running sound overlapping itself)
        if (!s.source.isPlaying) 
        {
            s.source.Play();
        }
    }

    // Stop method (useful for stopping the running sound when the player stops moving)
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        
        s.source.Stop();
    }

    // Special method for shooters: allows multiple gunshots to overlap without cutting off
    public void PlayOneShot(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.PlayOneShot(s.clip);
    }
}