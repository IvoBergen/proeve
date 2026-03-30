using UnityEngine;

public class Movecamera : MonoBehaviour
{
    public Transform camPosition;

    public void Start()
    {
        transform.position = camPosition.position;
    }
}
