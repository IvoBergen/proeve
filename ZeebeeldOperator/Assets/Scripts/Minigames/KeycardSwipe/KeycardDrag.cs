using UnityEngine;
using bnyhtz;

public class KeycardDrag : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private KeycardScanner _scanner;
        [SerializeField] private Collider _cardCollider;
        [SerializeField] private Transform _dragPlaneReference;
        [SerializeField] private float _planeForwardOffset = 0.05f;
        [SerializeField] private float _scrollSensitivity = 0.35f;
        [SerializeField] private LayerMask _cardCollisionMask = ~0;
        [SerializeField] private float _cardProbeRadius = 0.05f;
        [SerializeField] private float _wallPadding = 0.02f;

        private bool _held;
        private Plane _dragPlane;
        private Vector3 _dragPlaneNormal;
        private Vector3 _planarGrabOffset;
        private float _normalOffset;
        private const float MinMoveDistance = 0.0001f;

        private void Awake()
        {
            if (_camera == null)
            {
                _camera = Camera.main;
            }

            if (_cardCollider == null)
            {
                _cardCollider = GetComponent<Collider>();
            }
        }

        private void Update()
        {
            if (_scanner != null && !_scanner.IsInteractionActive)
            {
                _held = false;
                return;
            }

            UpdateDragPlane();
            ApplyScroll();

            if (Input.GetMouseButtonDown(0))
            {
                TryGrabCard();
            }

            if (Input.GetMouseButtonUp(0))
            {
                _held = false;
            }

            if (_held)
            {
                UpdateDraggedPosition();
            }
        }

        private void ApplyScroll()
        {
            float scrollInput = Input.mouseScrollDelta.y;
            if (Mathf.Abs(scrollInput) <= Mathf.Epsilon)
            {
                return;
            }

            Vector3 lookDirection = _camera != null ? _camera.transform.forward : transform.forward;
            float scrollDelta = scrollInput * _scrollSensitivity;

            if (_held)
            {
                // Project look-direction scroll onto plane normal so held dragging stays stable.
                float lookAlignment = Vector3.Dot(lookDirection, _dragPlaneNormal);
                if (Mathf.Abs(lookAlignment) > 0.001f)
                {
                    _normalOffset += scrollDelta * lookAlignment;
                }
                return;
            }

            MoveCardTo(transform.position + (lookDirection * scrollDelta));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_scanner == null)
            {
                return;
            }

            _scanner.NotifyCardZoneEnter(other);
        }

        private void TryGrabCard()
        {
            if (_camera == null || _cardCollider == null)
            {
                return;
            }

            Ray ray = _camera.ScreenPointToRay(GetPointerScreenPosition());
            RaycastHit[] hits = Physics.RaycastAll(ray);
            if (hits == null || hits.Length == 0)
            {
                return;
            }

            bool cardHitFound = false;
            float closestCardHitDistance = float.MaxValue;

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider != _cardCollider)
                {
                    continue;
                }

                if (hits[i].distance < closestCardHitDistance)
                {
                    closestCardHitDistance = hits[i].distance;
                    cardHitFound = true;
                }
            }

            if (!cardHitFound)
            {
                return;
            }

            if (!TryGetPointOnDragPlane(ray, out Vector3 planePoint))
            {
                return;
            }

            _held = true;
            _planarGrabOffset = Vector3.zero;
            _normalOffset = 0f;
        }

        private void UpdateDraggedPosition()
        {
            if (_camera == null)
            {
                return;
            }

            Ray ray = _camera.ScreenPointToRay(GetPointerScreenPosition());
            if (!TryGetPointOnDragPlane(ray, out Vector3 planePoint))
            {
                return;
            }

            MoveCardTo(planePoint + _planarGrabOffset + (_dragPlaneNormal * _normalOffset));
        }

        private void UpdateDragPlane()
        {
            Vector3 normal;
            Vector3 point;

            if (_dragPlaneReference != null)
            {
                normal = _dragPlaneReference.forward;
                point = _dragPlaneReference.position + (_dragPlaneReference.forward * _planeForwardOffset);
            }
            else if (_scanner != null)
            {
                normal = _scanner.transform.forward;
                point = _scanner.transform.position + (_scanner.transform.forward * _planeForwardOffset);
            }
            else
            {
                normal = Vector3.forward;
                point = transform.position;
            }

            _dragPlaneNormal = normal.normalized;
            _dragPlane = new Plane(_dragPlaneNormal, point);
        }

        private bool TryGetPointOnDragPlane(Ray ray, out Vector3 point)
        {
            if (_dragPlane.Raycast(ray, out float enter))
            {
                point = ray.GetPoint(enter);
                return true;
            }

            point = default;
            return false;
        }

        private Vector3 GetPointerScreenPosition()
        {
            return new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
        }

        private void MoveCardTo(Vector3 desiredPosition)
        {
            Vector3 currentPosition = transform.position;
            Vector3 toDesired = desiredPosition - currentPosition;
            float distance = toDesired.magnitude;

            if (distance <= MinMoveDistance)
            {
                transform.position = desiredPosition;
                return;
            }

            Vector3 direction = toDesired / distance;
            Vector3 resolvedPosition = desiredPosition;
            RaycastHit[] hits = Physics.SphereCastAll(
                currentPosition,
                _cardProbeRadius,
                direction,
                distance,
                _cardCollisionMask,
                QueryTriggerInteraction.Ignore
            );

            if (hits.Length > 0)
            {
                float closestDistance = float.MaxValue;

                for (int i = 0; i < hits.Length; i++)
                {
                    Collider hitCollider = hits[i].collider;
                    if (hitCollider == null)
                    {
                        continue;
                    }

                    if (hitCollider == _cardCollider || hitCollider.transform.IsChildOf(transform))
                    {
                        continue;
                    }

                    if (hits[i].distance < closestDistance)
                    {
                        closestDistance = hits[i].distance;
                    }
                }

                if (closestDistance < float.MaxValue)
                {
                    float safeDistance = Mathf.Max(closestDistance - _wallPadding, 0f);
                    resolvedPosition = currentPosition + (direction * safeDistance);
                }
            }

            transform.position = resolvedPosition;
        }
    }
