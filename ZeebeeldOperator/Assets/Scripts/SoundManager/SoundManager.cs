using System.Collections.Generic;
using UnityEngine;

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
    private AudioSource globalSource;

    private void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); return; }
        globalSource = gameObject.AddComponent<AudioSource>();
        globalSource.playOnAwake = false;
        globalSource.spatialBlend = 0;
        soundDictionary = new Dictionary<string, AudioClip>();
        foreach (var sound in soundList)
        {
            if (!soundDictionary.ContainsKey(sound.name))
                soundDictionary.Add(sound.name, sound.clip);
        }
    }

    public void Play(string soundName)
    {
        PlayWithVolume(soundName, 1.0f);
    }
    public void PlayWithVolume(string soundName, float volume)
    {
        if (soundDictionary.TryGetValue(soundName, out AudioClip clip))
        {
            globalSource.PlayOneShot(clip, volume);
        }
    }
}