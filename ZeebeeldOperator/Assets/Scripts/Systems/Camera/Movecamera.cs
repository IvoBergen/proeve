using UnityEngine;

/// <summary>
/// <c>Movecamera</c> Moves the camera based on the set object in the inspector
/// </summary>
public class Movecamera : MonoBehaviour
{
    public Transform camPosition;

    public void Start()
    {
        transform.position = camPosition.position;
    }
}
