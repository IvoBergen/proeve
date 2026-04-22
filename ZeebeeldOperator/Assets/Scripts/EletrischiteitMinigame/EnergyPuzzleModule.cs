using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles the logic of the energy puzzle
/// </summary>
public class EnergyPuzzleModule : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private GameObject _puzzleVisualParent;

    [Header("Corner Anchors")]
    public Transform bottomLeftAnchor;
    public Transform topRightAnchor;

    [Header("Grid Settings")]
    public int width = 10;
    public int height = 10;
    [Range(0.5f, 1.5f)]
    public float spacingMultiplier = 1.0f;
    [SerializeField] private float winDelay = 1f;

    [Header("Prefabs")]
    public GameObject straightPrefab;
    public GameObject crossPrefab;
    public GameObject cornerPrefab;

    [Header("Events")]
    public UnityEvent OnPuzzleSolved;

    private PuzzleTile[,] _grid;
    private bool _isSolved = false;

    public void StartPuzzle()
    {
        if (_puzzleVisualParent != null)
            _puzzleVisualParent.SetActive(true);

        GeneratePuzzle();
    }

    public void GeneratePuzzle()
    {
        if (bottomLeftAnchor == null || topRightAnchor == null) return;

        foreach (Transform child in transform)
            Destroy(child.gameObject);

        _grid = new PuzzleTile[width, height];
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

                _grid[x, y] = tile;

                bool isGoal = (x == width - 1 && y == height - 1);
                tile.Init(this, isGoal);
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
            if (curr.x < width - 1 && (Random.value > 0.5f || curr.y == height - 1))
                curr.x++;
            else
                curr.y++;

            p.Add(curr);
        }

        return p;
    }

    GameObject GetRequiredPiece(List<Vector2Int> path, Vector2Int curr)
    {
        int i = path.IndexOf(curr);

        if (i <= 0 || i >= path.Count - 1)
            return straightPrefab;

        Vector2Int prev = path[i - 1];
        Vector2Int next = path[i + 1];

        return (prev.x == next.x || prev.y == next.y)
            ? straightPrefab
            : cornerPrefab;
    }

    GameObject PickRandomPrefab()
    {
        int r = Random.Range(0, 3);

        if (r == 0) return straightPrefab;
        if (r == 1) return cornerPrefab;

        return crossPrefab;
    }

    public void CheckConnection()
    {
        if (_isSolved) return;

        foreach (var t in _grid)
        {
            if (t != null)
                t.SetPowerVisual(false);
        }

        FlowPower(0, 0, new List<PuzzleTile>());
    }

    void FlowPower(int x, int y, List<PuzzleTile> visited)
    {
        if (x < 0 || x >= width || y < 0 || y >= height) return;

        PuzzleTile curr = _grid[x, y];

        if (curr == null || visited.Contains(curr)) return;

        visited.Add(curr);
        curr.SetPowerVisual(true);

        if (x == width - 1 && y == height - 1)
        {
            _isSolved = true;
            StartCoroutine(HandleWinSequence());
            return;
        }

        if (curr.North && y + 1 < height && _grid[x, y + 1].South)
            FlowPower(x, y + 1, visited);

        if (curr.South && y - 1 >= 0 && _grid[x, y - 1].North)
            FlowPower(x, y - 1, visited);

        if (curr.East && x + 1 < width && _grid[x + 1, y].West)
            FlowPower(x + 1, y, visited);

        if (curr.West && x - 1 >= 0 && _grid[x - 1, y].East)
            FlowPower(x - 1, y, visited);
    }

    private IEnumerator HandleWinSequence()
    {
        yield return new WaitForSeconds(winDelay);
        OnPuzzleSolved.Invoke();
    }

    void ScrambleBoard()
    {
        foreach (var t in _grid)
        {
            int r = Random.Range(1, 4);

            for (int i = 0; i < r; i++)
                t.RotateTile();
        }
    }

    public void OnVictory()
    {
        if (_puzzleVisualParent != null)
            _puzzleVisualParent.SetActive(false);
    }
}