using UnityEngine;

/// <summary>
/// <c>ArrowMover</c> Moves the arrow along the grid.
/// </summary>
public class ArrowMover : MonoBehaviour
{
    [SerializeField] private Transform _arrowPoint;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _speed = 1f;

    private float _t = 0f;
    private int _direction = 1;

    private void Awake()
    {
        this.enabled = false;
    }
    void Update()
    {

        _t += Time.deltaTime * _speed * _direction;

        if (_t >= 1f)
        {
            _t = 1f; _direction = -1;
        }

        if (_t <= 0f)
        {
            _t = 0f; _direction = 1;
        }


        _arrowPoint.position = Vector3.Lerp(_startPoint.position, _endPoint.position, _t);
    }
}
