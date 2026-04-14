using UnityEngine;

public class EnergyPuzzleLightControler : MonoBehaviour
{
    [SerializeField] private MeshRenderer lightRenderer;

    public void MazeComplete()
    {
        if (lightRenderer != null)
        {
            lightRenderer.material.color = Color.green;
        }
        else
        {
            Debug.LogError("Light Renderer not assigned on " + gameObject.name);
        }
    }
}