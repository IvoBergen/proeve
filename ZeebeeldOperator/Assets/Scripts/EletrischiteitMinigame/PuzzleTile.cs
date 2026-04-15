using UnityEngine;

/// <summary>
/// Stores information of wich way the tile is facing 
/// </summary>
public class PuzzleTile : MonoBehaviour
{
    [Header("Initial Connections (At 0 Rotation)")]
    public bool North;
    public bool East;
    public bool South;
    public bool West;

    [Tooltip("Check this if the logic moves opposite to the visual spin")]
    public bool reverseRotationLogic = false;

    private EnergyPuzzleModule _module;
    private int _rotationStep = 0;

    public void Init(EnergyPuzzleModule module)
    {
        _module = module;
        _rotationStep = Mathf.RoundToInt(transform.localEulerAngles.x / 90f) % 4;
        ApplyRotation();
    }

    private void OnMouseDown()
    {
        RotateTile();
        if (_module != null) _module.CheckConnection();
    }

    public void RotateTile()
    {
        _rotationStep = (_rotationStep + 1) % 4;
        ApplyRotation();
        if (reverseRotationLogic)
        {
            bool oldNorth = North;
            North = East;
            East = South;
            South = West;
            West = oldNorth;
        }
        else
        {
            bool oldNorth = North;
            North = West;
            West = South;
            South = East;
            East = oldNorth;
        }
    }

    private void ApplyRotation()
    {
        transform.localEulerAngles = new Vector3(_rotationStep * 90f, -90f, -90f);
    }

    public void SetPowerVisual(bool state)
    {
        MeshRenderer mr = GetComponentInChildren<MeshRenderer>();
        if (mr != null) mr.material.color = state ? Color.yellow : Color.white;
    }
    public void SetColorManual(Color c)
    {
        MeshRenderer mr = GetComponentInChildren<MeshRenderer>();
        if (mr != null) mr.material.color = c;
    }
}