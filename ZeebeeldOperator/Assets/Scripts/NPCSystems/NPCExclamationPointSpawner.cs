using UnityEngine;

namespace bnyhtz
{
    /// <summary>
    /// Spawns a hovering exclamation point atop an NPC to indicate they have a quest or dialogue available. It can be permanently removed after the NPC has been interacted with.
    /// </summary>
    public class NPCExclamationPointSpawner : MonoBehaviour
    {
        [Header("Spawn")]
        [SerializeField] private GameObject _exclamationPrefab;
        [SerializeField] private bool _manualSpawn;

        [Header("Position")]
        [SerializeField] private float _heightOffset = 2f;
        [SerializeField] private float _hoverHeight = 0.25f;
        [SerializeField] private float _hoverSpeed = 2f;

        private GameObject _spawnedExclamation;
        private bool _destroyed;

        private void Start()
        {
            if (_manualSpawn) return;

            SpawnExclamation();
        }

        private void Update()
        {
            if (_destroyed) return;

            HoverExclamation();
        }

        public void SpawnExclamation()
        {
            if (_destroyed || _spawnedExclamation != null || _exclamationPrefab == null) return;
            if (_manualSpawn && _spawnedExclamation != null)
            {
                _spawnedExclamation = Instantiate(_exclamationPrefab, GetBasePosition(), Quaternion.identity);
            }
            _spawnedExclamation = Instantiate(_exclamationPrefab, GetBasePosition(), Quaternion.identity);
        }

        private void HoverExclamation()
        {
            if (_spawnedExclamation == null) return;

            float hoverOffset = Mathf.Sin(Time.time * _hoverSpeed) * _hoverHeight;
            _spawnedExclamation.transform.position = GetBasePosition() + Vector3.up * hoverOffset;
        }

        private Vector3 GetBasePosition()
        {
            return transform.position + Vector3.up * _heightOffset;
        }

        private void RemoveExclamation()
        {
            if (_spawnedExclamation == null) return;

            Destroy(_spawnedExclamation);
            _spawnedExclamation = null;
        }
        
        public void DestroyExclamation()
        {
            _destroyed = true;
            RemoveExclamation();
        }
    }
}
