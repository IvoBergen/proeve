using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the master audio volume.
/// </summary>
public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _text;

    private void Start()
    {
        _slider.minValue = 0;
        _slider.maxValue = 100;
        _slider.wholeNumbers = true;
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 50);
        _slider.value = savedVolume;
        AudioListener.volume = savedVolume / 100f;
        UpdateText(savedVolume);
        _slider.onValueChanged.AddListener(ChangeVolume);
    }

    private void ChangeVolume(float value)
    {
        AudioListener.volume = value / 100f;
        PlayerPrefs.SetFloat("MasterVolume", value);
        PlayerPrefs.Save();
        UpdateText(value);
    }

    private void UpdateText(float value)
    {
        _text.text = value.ToString("0") + "%";
    }
}