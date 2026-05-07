using UnityEngine;

/// <summary>
/// <c>FPSController</c> Controls the FPS based on the refreshratio of the pc/laptop
/// </summary>
public class FPSController : MonoBehaviour
{
    void Start()
    {
        RefreshRate currentRate = Screen.currentResolution.refreshRateRatio;

        Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.FullScreenWindow, currentRate);

        Application.targetFrameRate = (int)currentRate.value;

        QualitySettings.vSyncCount = 1;
    }
}
