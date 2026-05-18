using UnityEngine;

namespace bnyhtz
{
    /// <summary>
    /// Spawns an exclamation object above an NPC while the player is nearby.
    /// </summary>
    public class NPCExclamation : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private GameObject _exclamationPrefab;
        [SerializeField] private float _range = 5f;
        [SerializeField] private float _heightOffset = 2f;

        private GameObject _spawnedExclamation;
        private bool _destroyed;

        private void Update()
        {
            if (_destroyed || _player == null || _exclamationPrefab == null) return;

            bool playerInRange = Vector3.Distance(transform.position, _player.position) <= _range;

            if (playerInRange)
            {
                SpawnExclamation();
                return;
            }

            RemoveExclamation();
        }

        private void SpawnExclamation()
        {
            if (_spawnedExclamation != null) return;

            Vector3 spawnPosition = transform.position + Vector3.up * _heightOffset;
            _spawnedExclamation = Instantiate(_exclamationPrefab, spawnPosition, Quaternion.identity);
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
