using UnityEngine;
using UnityEngine.UI;

public class BinocularSystem : MonoBehaviour
{
    public enum Direction
    {
        North,
        East,
        South,
        West
    }

    [Header("UI")]
    [SerializeField] private Image _displayArea;
    [SerializeField] private RectTransform _photoRect;

    [Header("North")]
    [SerializeField] private Sprite _northSprite;

    [Header("East")]
    [SerializeField] private Sprite _eastSprite;

    [Header("South")]
    [SerializeField] private Sprite _southSprite;

    [Header("West")]
    [SerializeField] private Sprite _westSprite;

    private Direction _currentDirection = Direction.North;

    private void Start()
    {
        ApplyView();
    }

    // BUTTON FUNCTIONS
    public void SetNorth()
    {
        _currentDirection = Direction.North;
        ApplyView();
    }

    public void SetEast()
    {
        _currentDirection = Direction.East;
        ApplyView();
    }

    public void SetSouth()
    {
        _currentDirection = Direction.South;
        ApplyView();
    }

    public void SetWest()
    {
        _currentDirection = Direction.West;
        ApplyView();
    }

    private void ApplyView()
    {
        if (_displayArea == null)
            return;

        if (_currentDirection == Direction.North)
        {
            _displayArea.sprite = _northSprite;
        }
        else if (_currentDirection == Direction.East)
        {
            _displayArea.sprite = _eastSprite;
        }
        else if (_currentDirection == Direction.South)
        {
            _displayArea.sprite = _southSprite;
        }
        else if (_currentDirection == Direction.West)
        {
            _displayArea.sprite = _westSprite;
        }

        if (_photoRect != null)
        {
            _photoRect.localPosition = Vector3.zero;
        }
    }
}