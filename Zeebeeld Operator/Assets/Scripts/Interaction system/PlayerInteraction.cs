using UnityEngine;

/// <summary>
/// Detects interactables using a spherecast and handles interaction input.
/// Disabled automatically during dialogue.
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

    private IInterface _currentInteractable;
    private bool _hasInteracted = false;

    void Update()
    {
        if (DialogueManager.Instance != null && DialogueManager.Instance.IsDialogueActive)
        {
            _currentInteractable = null;
            _hasInteracted = false;
            _canvasHolder.SetActive(false);
            return;
        }

        DetectInteractable();

        _canvasHolder.SetActive(_currentInteractable != null && !_hasInteracted);

        if (Input.GetKeyDown(KeyCode.E) && _currentInteractable != null)
        {
            _currentInteractable.Interact();
            _hasInteracted = true;
        }
    }

    private void DetectInteractable()
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, _radius, out hit, _range, _interactableLayer))
        {
            IInterface interactable = hit.collider.GetComponent<IInterface>();

            if (interactable != null)
            {
                if (_currentInteractable != interactable)
                {
                    _hasInteracted = false;
                }

                _currentInteractable = interactable;
                return;
            }
        }

        _currentInteractable = null;
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
}