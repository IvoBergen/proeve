using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the camera sensitivity slider.
/// </summary>
public class CamSenSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private PlayerCam playerCam;
    [SerializeField] private TMP_Text _text;

    void Start()
    {
        // Set slider to current sensitivity value and update text
        _slider.value = playerCam.sensX;
        _text.text = playerCam.sensX.ToString("F1");

        _slider.onValueChanged.AddListener(OnSliderChanged);
    }

    private void OnSliderChanged(float value)
    {
        playerCam.sensX = value;
        playerCam.sensY = value;
        _text.text = value.ToString("F1");
    }
}