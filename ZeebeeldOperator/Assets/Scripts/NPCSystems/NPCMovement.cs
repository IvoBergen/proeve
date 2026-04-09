using UnityEngine;

/// <summary>
/// <c>NPCMovement</c> Handles the movement of the NPC.
/// </summary>
public class NPCMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _movementSpeed = 15f;
    [SerializeField] private float _waitTime = 2f;
    public Transform[] points;

    public NPCState State { get; private set; } = NPCState.Rotating;
    public Transform Target => _target;

    private int _point;
    private Transform _target;
    private int _direction = 1;
    private float _waitTimer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _target = points[_point];
    }

    private void Update()
    {
        switch (State)
        {
            case NPCState.Rotating:

                break;

            case NPCState.Moving:
                Vector3 dir = (_target.position - transform.position).normalized;
                _rb.MovePosition(transform.position + dir * (_movementSpeed * Time.deltaTime));

                if (Vector3.Distance(_target.position, transform.position) <= 0.1f)
                {
                    transform.position = _target.position;
                    State = NPCState.Waiting;
                    _waitTimer = _waitTime;
                }
                break;

            case NPCState.Waiting:
                _waitTimer -= Time.deltaTime;
                if (_waitTimer <= 0f)
                    AdvanceToNextPoint();
                break;
        }
    }

    /// <summary>
    /// <c>AdvanceToNextPoint</c> Ensures the Npc moves to the next point in the list.
    /// </summary>
    private void AdvanceToNextPoint()
    {
        _point += _direction;

        if (_point >= points.Length || _point < 0)
        {
            _direction *= -1;
            _point += _direction * 2;
        }

        _target = points[_point];
        State = NPCState.Rotating;
    }

    /// <summary>
    /// <c>SetMoving</c> Handles changing the state of the NPC
    /// </summary>
    public void SetMoving()
    {
        if (State == NPCState.Rotating)
            State = NPCState.Moving;
    }
}