using UnityEngine;
using bnyhtz;

/// <summary>
/// Detects nearby interactables, shows the interaction prompt,
/// and invokes interactions on E. Interaction is paused during dialogue
/// and while the clipboard is open.
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _range = 3f;
    [SerializeField] private float _radius = 0.1f;
    [SerializeField] private LayerMask _interactableLayer;

    [Header("References")]
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _canvasHolder;
    [SerializeField] private DialogueManager _dialogueManager;
    [SerializeField] private GameStateManager _gameStateManager;
    [SerializeField] private KeycardScanner _keycardScanner;

    private IInterface[] _currentInteractables;
    private bool _hasInteracted = false;

    void Update()
    {
        if (_gameStateManager != null && _gameStateManager.IsClipboardOpen)
        {
            _currentInteractables = null;
            _hasInteracted = false;
            _canvasHolder.SetActive(false);
            return;
        }

        if (_dialogueManager.Active)
        {
            _currentInteractables = null;
            _hasInteracted = false;
            _canvasHolder.SetActive(false);
            return;
        }

        if (_keycardScanner != null && _keycardScanner.IsInteractionActive)
        {
            _currentInteractables = null;
            _hasInteracted = false;
            _canvasHolder.SetActive(false);
            return;
        }

        DetectInteractable();

        _canvasHolder.SetActive(_currentInteractables != null && !_hasInteracted);

        if (Input.GetKeyDown(KeyCode.E) && _currentInteractables != null)
        {
            foreach (var interactable in _currentInteractables)
            {
                interactable.Interact();
            }
            _hasInteracted = true;
        }
    }

    private void DetectInteractable()
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, _radius, out hit, _range, _interactableLayer))
        {
            IInterface[] interactables = hit.collider.GetComponents<IInterface>();

            if (interactables.Length > 0)
            {
                if (_currentInteractables != interactables)
                    _hasInteracted = false;

                _currentInteractables = interactables;
                return;
            }
        }

        _currentInteractables = null;
        _hasInteracted = false;
    }

    private void OnDrawGizmos()
    {
        if (_camera == null) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(_camera.transform.position, _camera.transform.forward * _range);

        Gizmos.DrawWireSphere(
            _camera.transform.position + _camera.transform.forward * _range,
            _radius
        );
    }
    public void StopShowingInteraction()
    {
        _dialogueManager.Active = true;
    }
    public void StartShowingInteraction()
    {
        _dialogueManager.Active = false;
    }

    private void Awake()
    {
        if (_gameStateManager == null)
        {
            _gameStateManager = FindObjectOfType<GameStateManager>();
        }

        if (_keycardScanner == null)
        {
            _keycardScanner = FindObjectOfType<KeycardScanner>();
        }
    }
}

