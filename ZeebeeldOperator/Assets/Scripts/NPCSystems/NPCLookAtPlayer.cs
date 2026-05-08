using UnityEngine;

/// <summary>
/// Handles NPC head rotation towards the player.
/// </summary>

namespace bnyhtz
{
 public class NPCLookAtPlayer : MonoBehaviour
    {
        [SerializeField] private Transform _npcHead;
        [SerializeField] private Transform _player;
        [SerializeField] private float _lookRange = 5f;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private float _maxHorizontalAngle = 70f;
        [SerializeField] private float _maxVerticalAngle = 35f;

        private Quaternion _normalRotation;

        private void Awake()
        {
            if (_npcHead == null) return;

            _normalRotation = _npcHead.localRotation;
        }

        private void Update()
        {
            if (_npcHead == null || _player == null) return;

            if (PlayerInRange())
            {
                LookAtPlayer();
                return;
            }

            ResetHeadRotation();
        }

        private bool PlayerInRange()
        {
            return Vector3.Distance(_npcHead.position, _player.position) <= _lookRange;
        }

        private void LookAtPlayer()
        {
            Vector3 direction = _player.position - _npcHead.position;

            if (direction.sqrMagnitude < 0.001f) return;

            Quaternion targetWorldRotation = Quaternion.LookRotation(direction);
            Quaternion targetLocalRotation = GetLocalRotation(targetWorldRotation);
            Quaternion limitedRotation = LimitRotation(targetLocalRotation);

            _npcHead.localRotation = Quaternion.Slerp(
                _npcHead.localRotation,
                limitedRotation,
                _rotationSpeed * Time.deltaTime);
        }

        private Quaternion GetLocalRotation(Quaternion targetWorldRotation)
        {
            if (_npcHead.parent == null)
            {
                return targetWorldRotation;
            }

            return Quaternion.Inverse(_npcHead.parent.rotation) * targetWorldRotation;
        }

        private Quaternion LimitRotation(Quaternion targetRotation)
        {
            Quaternion relativeRotation = Quaternion.Inverse(_normalRotation) * targetRotation;
            Vector3 targetAngles = relativeRotation.eulerAngles;

            float horizontalAngle = ClampAngle(targetAngles.y, -_maxHorizontalAngle, _maxHorizontalAngle);
            float verticalAngle = ClampAngle(targetAngles.x, -_maxVerticalAngle, _maxVerticalAngle);

            return _normalRotation * Quaternion.Euler(verticalAngle, horizontalAngle, 0f);
        }

        private float ClampAngle(float angle, float min, float max)
        {
            if (angle > 180f)
            {
                angle -= 360f;
            }

            return Mathf.Clamp(angle, min, max);
        }

        private void ResetHeadRotation()
        {
            _npcHead.localRotation = Quaternion.Slerp(
                _npcHead.localRotation,
                _normalRotation,
                _rotationSpeed * Time.deltaTime);
        }   
    }
}