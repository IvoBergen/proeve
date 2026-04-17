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
    private Dictionary<string, AudioClip> _soundDictionary;
    private Dictionary<string, AudioSource> _loopingSources;
    private AudioSource _globalSource;

    private void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); return; }

        _globalSource = gameObject.AddComponent<AudioSource>();
        _globalSource.playOnAwake = false;
        _globalSource.spatialBlend = 0;

        _soundDictionary = new Dictionary<string, AudioClip>();
        _loopingSources = new Dictionary<string, AudioSource>();

        foreach (var sound in soundList)
        {
            if (!string.IsNullOrEmpty(sound.name) && !_soundDictionary.ContainsKey(sound.name))
                _soundDictionary.Add(sound.name, sound.clip);
        }
    }
    // use this function in your event
    public void Play(string soundName)
    {
        if (_soundDictionary.TryGetValue(soundName, out AudioClip clip))
        {
            _globalSource.PlayOneShot(clip, 1.0f);
        }
    }
    // use this function the trigger sound that loops
    public void PlayLoop(string soundName)
    {
        if (_loopingSources.ContainsKey(soundName)) return;

        if (_soundDictionary.TryGetValue(soundName, out AudioClip clip))
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.clip = clip;
            newSource.loop = true;
            newSource.playOnAwake = false;
            newSource.Play();

            _loopingSources.Add(soundName, newSource);
        }
    }
    // use this to end looping
    public void StopLoop(string soundName)
    {
        if (_loopingSources.TryGetValue(soundName, out AudioSource source))
        {
            source.Stop();
            Destroy(source);
            _loopingSources.Remove(soundName);
        }
    }

    // use this function to trigger inside of a other script
    public void PlayWithVolume(string soundName, float volume)
    {
        if (_soundDictionary.TryGetValue(soundName, out AudioClip clip))
        {
            _globalSource.PlayOneShot(clip, volume);
        }
    }
}