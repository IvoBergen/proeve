using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// used to store all sound effects and play them with the string name (might change this to work dialogue)
/// </summary>
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [System.Serializable]
    public struct SoundEffect
    {
        public string name;
        public AudioClip clip;
    }

    public List<SoundEffect> soundList;
    private Dictionary<string, AudioClip> soundDictionary;
    private Dictionary<string, AudioSource> loopingSources;
    private AudioSource globalSource;

    private void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); return; }

        globalSource = gameObject.AddComponent<AudioSource>();
        globalSource.playOnAwake = false;
        globalSource.spatialBlend = 0;

        soundDictionary = new Dictionary<string, AudioClip>();
        loopingSources = new Dictionary<string, AudioSource>();

        foreach (var sound in soundList)
        {
            if (!string.IsNullOrEmpty(sound.name) && !soundDictionary.ContainsKey(sound.name))
                soundDictionary.Add(sound.name, sound.clip);
        }
    }
    // use this function in your event
    public void Play(string soundName)
    {
        if (soundDictionary.TryGetValue(soundName, out AudioClip clip))
        {
            globalSource.PlayOneShot(clip, 1.0f);
        }
    }
    // use this function the trigger sound that loops
    public void PlayLoop(string soundName)
    {
        if (loopingSources.ContainsKey(soundName)) return;

        if (soundDictionary.TryGetValue(soundName, out AudioClip clip))
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.clip = clip;
            newSource.loop = true;
            newSource.playOnAwake = false;
            newSource.Play();

            loopingSources.Add(soundName, newSource);
        }
    }
    // use this to end looping
    public void StopLoop(string soundName)
    {
        if (loopingSources.TryGetValue(soundName, out AudioSource source))
        {
            source.Stop();
            Destroy(source);
            loopingSources.Remove(soundName);
        }
    }

    // use this function to trigger inside of a other script
    public void PlayWithVolume(string soundName, float volume)
    {
        if (soundDictionary.TryGetValue(soundName, out AudioClip clip))
        {
            globalSource.PlayOneShot(clip, volume);
        }
    }
}