using UnityEngine;
/// <summary>
/// does movement for the ship
/// </summary>
public class schipMovement : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;

    [Header("Settings")]
    [SerializeField] private float _speed = 5f;

    private Transform _currentTarget;

    private void Start()
    {
        _currentTarget = _pointA;
    }

    private void Update()
    {
        MoveBetweenPoints();
    }

    private void MoveBetweenPoints()
    {
        // Beweeg richting target
        transform.position = Vector3.MoveTowards(
            transform.position,
            _currentTarget.position,
            _speed * Time.deltaTime
        );

        // Check of we dichtbij genoeg zijn om te switchen
        if (Vector3.Distance(transform.position, _currentTarget.position) < 0.1f)
        {
            _currentTarget = _currentTarget == _pointA ? _pointB : _pointA;
        }
    }
}