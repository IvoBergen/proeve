using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Creates the energy puzzle       
/// </summary>
public class EnergyPuzzleModule : MonoBehaviour
{
    public bool generateOnStart;
    public int width = 5;
    public int height = 5;
    public float spacing = 1.0f;

    public GameObject straightPrefab, crossPrefab, cornerPrefab;
    public UnityEvent OnPuzzleSolved;

    private PuzzleTile[,] grid;
    private bool _isSolved = false;

    void Start() { if (generateOnStart) GeneratePuzzle(); }

    public void GeneratePuzzle()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
        grid = new PuzzleTile[width, height];
        List<Vector2Int> path = GenerateAStarPath();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                GameObject prefab;
                if ((x == 0 && y == 0) || (x == width - 1 && y == height - 1))
                {
                    prefab = crossPrefab;
                }
                else
                {
                    prefab = path.Contains(pos) ? GetRequiredPiece(path, pos) : PickRandomPrefab();
                }

                Vector3 spawnPos = transform.TransformPoint(new Vector3(x * spacing, y * spacing, 0));
                GameObject go = Instantiate(prefab, spawnPos, transform.rotation, transform);
                go.transform.localEulerAngles = new Vector3(0f, -90f, -90f);

                PuzzleTile tile = go.GetComponent<PuzzleTile>();
                grid[x, y] = tile;
                tile.Init(this);
                if (x == width - 1 && y == height - 1)
                {
                    tile.SetColorManual(Color.red);
                }
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
        return (r == 0) ? straightPrefab : (r == 1 ? cornerPrefab : crossPrefab);
    }

    public void CheckConnection()
    {
        if (_isSolved) return;
        foreach (var t in grid) if (t != null) t.SetPowerVisual(false);
        grid[width - 1, height - 1].SetColorManual(Color.red);

        FlowPower(0, 0, new List<PuzzleTile>());
    }

    void FlowPower(int x, int y, List<PuzzleTile> visited)
    {
        if (x < 0 || x >= width || y < 0 || y >= height) return;
        PuzzleTile curr = grid[x, y];
        if (curr == null || visited.Contains(curr)) return;

        visited.Add(curr);
        curr.SetPowerVisual(true);
        if (x == width - 1 && y == height - 1)
        {
            _isSolved = true;
            curr.SetColorManual(Color.green);
            OnPuzzleSolved.Invoke();
            return;
        }

        if (curr.North && y + 1 < height && grid[x, y + 1].South) FlowPower(x, y + 1, visited);
        if (curr.South && y - 1 >= 0 && grid[x, y - 1].North) FlowPower(x, y - 1, visited);
        if (curr.East && x + 1 < width && grid[x + 1, y].West) FlowPower(x + 1, y, visited);
        if (curr.West && x - 1 >= 0 && grid[x - 1, y].East) FlowPower(x - 1, y, visited);
    }

    void ScrambleBoard()
    {
        foreach (var t in grid)
        {
            int r = Random.Range(0, 12);
            for (int i = 0; i < r; i++) t.RotateTile();
        }
    }
}