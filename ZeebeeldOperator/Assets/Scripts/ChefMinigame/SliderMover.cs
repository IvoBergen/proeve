using UnityEngine;

public class SliderMover : MonoBehaviour
{
    public Transform _sphere;
    public Transform _startPoint;
    public Transform _endPoint;
    public float _speed = 1f;

    private float _t = 0f;
    private int _direction = 1;

    void Update()
    {

        _t += Time.deltaTime * _speed * _direction;

        if (_t >= 1f) { _t = 1f; _direction = -1; }
        if (_t <= 0f) { _t = 0f; _direction = 1; }


        _sphere.position = Vector3.Lerp(_startPoint.position, _endPoint.position, _t);
    }
}
