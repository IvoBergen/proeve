using UnityEngine;

public class PuzzleTile : MonoBehaviour
{
    [Header("Initial Connections (At 0 Rotation)")]
    public bool North;
    public bool East;
    public bool South;
    public bool West;

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

        bool oldNorth = North;
        if (reverseRotationLogic)
        {
            North = East; East = South; South = West; West = oldNorth;
        }
        else
        {
            North = West; West = South; South = East; East = oldNorth;
        }
    }

    private void ApplyRotation()
    {
        transform.localEulerAngles = new Vector3(_rotationStep * 90f, -90f, -90f);
    }

    public void SetPowerVisual(bool state, Material activeMat, Material inactiveMat)
    {
        ApplyToAllSlots(state ? activeMat : inactiveMat);
    }

    public void SetMaterialManual(Material mat)
    {
        ApplyToAllSlots(mat);
    }

    private void ApplyToAllSlots(Material mat)
    {
        Renderer r = GetComponentInChildren<Renderer>();
        if (r == null || mat == null) return;

        // Swapping the entire array to ensure all 4 elements from your screenshot change
        Material[] newMats = new Material[r.sharedMaterials.Length];
        for (int i = 0; i < newMats.Length; i++)
        {
            newMats[i] = mat;
        }
        r.sharedMaterials = newMats;
    }
}