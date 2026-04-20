using UnityEngine;

namespace bnyhtz
{
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

        private bool _startZoneVisited;

        public bool IsInteractionActive => _interactionActive;
        public bool IsCompleted => _completed;

        private void Awake()
        {
            SetStatusLightCompleted(false);
            _interactionZone.enabled = true;
        }

        public void Interact()
        {
            if (_completed)
            {
                return;
            }

            _startZoneVisited = false;
            _interactionActive = true;
            SetStatusLightCompleted(false);
            _interactionZone.enabled = false;
            Debug.Log("Keycard minigame started.");
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
                Debug.Log("Keycard entered start zone.");
                return;
            }

            if (zone == _endZone && _startZoneVisited)
            {
                _completed = true;
                _interactionActive = false;
                SetStatusLightCompleted(true);
                Debug.Log("Keycard swipe completed.");
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
