using UnityEngine;

/// <summary>
/// Stores information of which way the tile is facing 
/// </summary>
public class PuzzleTile : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material _matWhite;
    [SerializeField] private Material _matYellow;
    [SerializeField] private Material _matRed;
    [SerializeField] private Material _matGreen;

    [Header("Initial Connections (At 0 Rotation)")]
    public bool North;
    public bool East;
    public bool South;
    public bool West;

    [Tooltip("Check this if the logic moves opposite to the visual spin")]
    public bool reverseRotationLogic = false;

    private EnergyPuzzleModule _module;
    private int _rotationStep = 0;
    private bool _isGoalTile = false;
    void Awake()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void Init(EnergyPuzzleModule module, bool isGoal = false)
    {
        _module = module;
        _isGoalTile = isGoal;
        if (_meshRenderer == null)
            _meshRenderer = GetComponentInChildren<MeshRenderer>();

        _rotationStep = Mathf.RoundToInt(transform.localEulerAngles.x / 90f) % 4;

        ApplyRotation();
        SetPowerVisual(false);
    }

    private void OnMouseDown()
    {
        RotateTile();

        if (_module != null)
            _module.CheckConnection();
    }

    public void RotateTile()
    {
        _rotationStep = (_rotationStep + 1) % 4;

        ApplyRotation();
        RotateConnections();
    }

    private void RotateConnections()
    {
        bool oldNorth = North;

        if (reverseRotationLogic)
        {
            North = East;
            East = South;
            South = West;
            West = oldNorth;
        }
        else
        {
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
        if (_meshRenderer == null)
        {
            Debug.LogWarning("No MeshRenderer found on " + gameObject.name);
            return;
        }

        if (_isGoalTile)
        {
            _meshRenderer.material = state ? _matGreen : _matRed;
        }
        else
        {
            _meshRenderer.material = state ? _matYellow : _matWhite;
        }
    }

    public void SetMaterial(Material mat)
    {
        if (_meshRenderer == null)
            _meshRenderer = GetComponentInChildren<MeshRenderer>();

        if (_meshRenderer != null)
            _meshRenderer.material = mat;
    }
}