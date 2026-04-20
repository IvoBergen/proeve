using UnityEngine;

/// <summary>
/// is used to show the puzzle is solved 
/// </summary>

public class EnergyPuzzleLightControler : MonoBehaviour
{
    [SerializeField] private MeshRenderer lightRenderer;

    public void MazeComplete()
    {
        if (lightRenderer != null)
        {
            lightRenderer.material.color = Color.green;
        }

    }
}