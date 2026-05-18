using UnityEngine;

namespace bnyhtz
{
    /// <summary>
    /// Spawns a hovering exclamation object above an NPC.
    /// </summary>
    public class NPCExclamation : MonoBehaviour
    {
        [SerializeField] private GameObject _exclamationPrefab;
        [SerializeField] private float _heightOffset = 2f;
        [SerializeField] private float _hoverHeight = 0.25f;
        [SerializeField] private float _hoverSpeed = 2f;

        private GameObject _spawnedExclamation;
        private Vector3 _spawnPosition;
        private bool _destroyed;

        private void Start()
        {
            SpawnExclamation();
        }

        private void Update()
        {
            if (_destroyed) return;
            
            HoverExclamation();
        }

        private void SpawnExclamation()
        {
            if (_exclamationPrefab == null) return;
            if (_spawnedExclamation != null) return;

            _spawnPosition = transform.position + Vector3.up * _heightOffset;
            _spawnedExclamation = Instantiate(_exclamationPrefab, _spawnPosition, Quaternion.identity);
        }

        private void HoverExclamation()
        {
            if (_spawnedExclamation == null) return;

            float hoverOffset = Mathf.Sin(Time.time * _hoverSpeed) * _hoverHeight;
            _spawnedExclamation.transform.position = _spawnPosition + Vector3.up * hoverOffset;
        }

        private void RemoveExclamation()
        {
            if (_spawnedExclamation == null) return;

            Destroy(_spawnedExclamation);
            _spawnedExclamation = null;
        }

        /// <summary>
        /// Permanently removes the exclamation object after the NPC has been interacted with.
        /// </summary>
        public void DestroyExclamation()
        {
            _destroyed = true;
            RemoveExclamation();
        }
    }
}
