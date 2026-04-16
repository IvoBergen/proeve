using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnergyPuzzleModule : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private GameObject puzzleVisualParent;

    [Header("Corner Anchors")]
    public Transform bottomLeftAnchor;
    public Transform topRightAnchor;

    [Header("Grid Settings")]
    public int width = 10;
    public int height = 10;
    [Range(0.5f, 1.5f)]
    public float spacingMultiplier = 1.0f;

    [Header("Prefabs")]
    public GameObject straightPrefab;
    public GameObject crossPrefab;
    public GameObject cornerPrefab;

    [Header("Events")]
    public UnityEvent OnPuzzleSolved;

    private PuzzleTile[,] grid;
    private bool _isSolved = false;
    public void StartPuzzle()
    {
        if (puzzleVisualParent != null) puzzleVisualParent.SetActive(true);
        GeneratePuzzle();
    }

    public void GeneratePuzzle()
    {
        if (bottomLeftAnchor == null || topRightAnchor == null)
        {
            Debug.LogError("Please assign the Bottom Left and Top Right Anchors in the Inspector!");
            return;
        }
        foreach (Transform child in transform) Destroy(child.gameObject);

        grid = new PuzzleTile[width, height];
        _isSolved = false;
        Vector3 diagonalVec = topRightAnchor.position - bottomLeftAnchor.position;
        float totalWidth = Vector3.Dot(diagonalVec, bottomLeftAnchor.right);
        float totalHeight = Vector3.Dot(diagonalVec, bottomLeftAnchor.up);

        float stepX = (width > 1) ? (totalWidth / (width - 1)) : 0;
        float stepY = (height > 1) ? (totalHeight / (height - 1)) : 0;
        List<Vector2Int> path = GenerateAStarPath();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                GameObject prefab;
                if ((x == 0 && y == 0) || (x == width - 1 && y == height - 1))
                    prefab = crossPrefab;
                else
                    prefab = path.Contains(pos) ? GetRequiredPiece(path, pos) : PickRandomPrefab();
                Vector3 offset = (bottomLeftAnchor.right * x * stepX * spacingMultiplier) +
                                 (bottomLeftAnchor.up * y * stepY * spacingMultiplier);

                Vector3 spawnPos = bottomLeftAnchor.position + offset;
                GameObject go = Instantiate(prefab, spawnPos, bottomLeftAnchor.rotation, transform);
                go.transform.Rotate(0, -90, -90, Space.Self);

                PuzzleTile tile = go.GetComponent<PuzzleTile>();
                grid[x, y] = tile;
                tile.Init(this);
                if (x == width - 1 && y == height - 1) tile.SetColorManual(Color.red);
            }
        }
        ScrambleBoard();
        CheckConnection();
    }
    List<Vector2Int> GenerateAStarPath()
    {
        List<Vector2Int> p = new List<Vector2Int>();
        Vector2Int curr = new Vector2Int(0, 0);
        p.Add(curr);
        while (curr.x < width - 1 || curr.y < height - 1)
        {
            if (curr.x < width - 1 && (Random.value > 0.5f || curr.y == height - 1)) curr.x++;
            else curr.y++;
            p.Add(curr);
        }
        return p;
    }

    GameObject GetRequiredPiece(List<Vector2Int> path, Vector2Int curr)
    {
        int i = path.IndexOf(curr);
        if (i <= 0 || i >= path.Count - 1) return straightPrefab;
        Vector2Int prev = path[i - 1];
        Vector2Int next = path[i + 1];
        return (prev.x == next.x || prev.y == next.y) ? straightPrefab : cornerPrefab;
    }

    GameObject PickRandomPrefab()
    {
        int r = Random.Range(0, 3);
        if (r == 0) return straightPrefab;
        if (r == 1) return cornerPrefab;
        return crossPrefab;
    }

    // --- Logic & Win Condition ---

    public void CheckConnection()
    {
        if (_isSolved) return;

        // Reset all visuals to "unpowered"
        foreach (var t in grid) if (t != null) t.SetPowerVisual(false);

        // Ensure the end target is red unless connected
        if (grid != null && grid.Length > 0) grid[width - 1, height - 1].SetColorManual(Color.red);

        // Start flow from [0,0]
        FlowPower(0, 0, new List<PuzzleTile>());
    }

    void FlowPower(int x, int y, List<PuzzleTile> visited)
    {
        if (x < 0 || x >= width || y < 0 || y >= height) return;
        PuzzleTile curr = grid[x, y];
        if (curr == null || visited.Contains(curr)) return;

        visited.Add(curr);
        curr.SetPowerVisual(true);

        // Victory Condition reached
        if (x == width - 1 && y == height - 1)
        {
            _isSolved = true;
            curr.SetColorManual(Color.green);
            OnPuzzleSolved.Invoke();
            return;
        }

        // Recursive flow check based on tile boolean directions
        if (curr.North && y + 1 < height && grid[x, y + 1].South) FlowPower(x, y + 1, visited);
        if (curr.South && y - 1 >= 0 && grid[x, y - 1].North) FlowPower(x, y - 1, visited);
        if (curr.East && x + 1 < width && grid[x + 1, y].West) FlowPower(x + 1, y, visited);
        if (curr.West && x - 1 >= 0 && grid[x - 1, y].East) FlowPower(x - 1, y, visited);
    }

    void ScrambleBoard()
    {
        foreach (var t in grid)
        {
            int r = Random.Range(1, 4); // Force at least one rotation
            for (int i = 0; i < r; i++) t.RotateTile();
        }
    }

    // Utility to hide board visually after completion
    public void OnVictory()
    {
        if (puzzleVisualParent != null) puzzleVisualParent.SetActive(false);
    }
}