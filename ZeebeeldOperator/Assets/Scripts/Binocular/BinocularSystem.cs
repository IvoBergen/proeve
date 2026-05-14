using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the binocular system by switching background sprites based on the looking direction.
/// </summary>
public class BinocularSystem : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image _displayArea;
    [SerializeField] private RectTransform _photoRect;

    [Header("Photo Sprites")]
    [SerializeField] private Sprite _imageNorth;
    [SerializeField] private Sprite _imageEast;
    [SerializeField] private Sprite _imageSouth;
    [SerializeField] private Sprite _imageWest;

    /// <summary>
    /// Checks for player input every frame to switch the view.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchView("North");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchView("East");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchView("West");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchView("South");
        }
    }

    /// <summary>
    /// Changes the displayed sprite based on a string direction and resets the photo position.
    /// </summary>
    /// <param name="direction">The cardinal direction to switch to (North, East, South, West).</param>
    public void SwitchView(string direction)
    {
        // Using &&/|| logic as per conventions if needed, though here we use string comparison.
        if (direction == "North")
        {
            _displayArea.sprite = _imageNorth;
        }
        else if (direction == "East")
        {
            _displayArea.sprite = _imageEast;
        }
        else if (direction == "West")
        {
            _displayArea.sprite = _imageWest;
        }
        else if (direction == "South")
        {
            _displayArea.sprite = _imageSouth;
        }

        _photoRect.localPosition = Vector3.zero;
    }
}