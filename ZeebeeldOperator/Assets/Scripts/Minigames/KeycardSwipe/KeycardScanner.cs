using UnityEngine;

namespace bnyhtz
{
    /// <summary>
    /// Manages keycard swipe interaction flow, validates swipe order, updates scanner feedback, and unlocks the door on success.
    /// </summary>
    public class KeycardScanner : MonoBehaviour, IInterface
    {
        [SerializeField] private bool _interactionActive;
        [SerializeField] private bool _completed;
        [SerializeField] private Collider _startZone;
        [SerializeField] private Collider _endZone;
        [SerializeField] private Renderer _statusLightRenderer;
        [SerializeField] private Material _redMaterial;
        [SerializeField] private Material _greenMaterial;
        [SerializeField] private BoxCollider _interactionZone;
        [SerializeField] public MeshRenderer _cardMeshRenderer;
        [SerializeField] private Door _door;
        [SerializeField] private GameObject _keycardUI;

        private bool _startZoneVisited;

        public bool IsInteractionActive => _interactionActive;
        public bool IsCompleted => _completed;

        private void Awake()
        {
            SetStatusLightCompleted(false);
            _interactionZone.enabled = true;
            _cardMeshRenderer.enabled = false;       
            _keycardUI.SetActive(false);
        }

        public void Interact()
        {
            if (_completed)
            {
                return;
            }

            _startZoneVisited = false;
            _interactionActive = true;
            _cardMeshRenderer.enabled = true;
            _keycardUI.SetActive(true);
            SetStatusLightCompleted(false);
            _interactionZone.enabled = false;
        }

        public void StopInteraction()
        {
            _interactionActive = false;
            _startZoneVisited = false;
        }

        public void NotifyCardZoneEnter(Collider zone)
        {
            if (!_interactionActive || zone == null)
            {
                return;
            }

            if (zone == _startZone)
            {
                _startZoneVisited = true;
                return;
            }

            if (zone == _endZone && _startZoneVisited)
            {
                _completed = true;
                _interactionActive = false;
                SetStatusLightCompleted(true);
                _door.OpenDoor();
                _cardMeshRenderer.enabled = false;       
                _keycardUI.SetActive(false);
            }
        }
    
        private void SetStatusLightCompleted(bool completed)
        {
            if (_statusLightRenderer == null)
            {
                return;
            }

            Material targetMaterial = completed ? _greenMaterial : _redMaterial;
            if (targetMaterial == null)
            {
                return;
            }

            _statusLightRenderer.material = targetMaterial;
        }
    }
}
