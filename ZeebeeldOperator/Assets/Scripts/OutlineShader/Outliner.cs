using System;
using System.Collections.Generic;
using UnityEngine;

public class Outliner : MonoBehaviour
{
    private static readonly int ColorProperty = Shader.PropertyToID("_color");
    private static readonly int ScaleProperty = Shader.PropertyToID("_scale");
    private static readonly int PulseSpeedProperty = Shader.PropertyToID("_pulsespeed");
    private static readonly int OnTimeProperty = Shader.PropertyToID("_ontime");
    private static readonly int OffTimeProperty = Shader.PropertyToID("_offtime");

    [Header("Default References")]
    [SerializeField] private Transform _player;
    [SerializeField] private Material _defaultOutlineMaterial;

    [Header("Default Settings")]
    [SerializeField] private float _defaultRange = 5f;
    [SerializeField] private Color _defaultColor = Color.yellow;
    [SerializeField] private float _defaultScale = 1.05f;
    [SerializeField] private float _defaultPulseSpeed = 5.3f;
    [SerializeField] private float _defaultOnTime = 0.52f;
    [SerializeField] private float _defaultOffTime = 0.25f;

    [Header("Targets")]
    [SerializeField] private List<OutlineTarget> _targets = new List<OutlineTarget>();

    private void Update()
    {
        if (_player == null)
        {
            DisableAllOutlines();
            return;
        }

        for (int i = 0; i < _targets.Count; i++)
        {
            OutlineTarget target = _targets[i];

            if (target == null)
            {
                continue;
            }

            target.UpdateOutline(
                _player,
                _defaultOutlineMaterial,
                _defaultRange,
                _defaultColor,
                _defaultScale,
                _defaultPulseSpeed,
                _defaultOnTime,
                _defaultOffTime);
        }
    }

    private void OnDisable()
    {
        DisableAllOutlines();
    }

    private void OnDestroy()
    {
        DisableAllOutlines();
    }

    private void DisableAllOutlines()
    {
        for (int i = 0; i < _targets.Count; i++)
        {
            if (_targets[i] != null)
            {
                _targets[i].DisableOutline();
            }
        }
    }

    [Serializable]
    private class OutlineTarget
    {
        [Header("Target")]
        [SerializeField] private GameObject _target;
        [SerializeField] private bool _forceEnabled;

        [Header("Override Material")]
        [SerializeField] private bool _overrideMaterial;
        [SerializeField] private Material _outlineMaterial;

        [Header("Override Range")]
        [SerializeField] private bool _overrideRange;
        [SerializeField] private float _range = 5f;

        [Header("Override Color")]
        [SerializeField] private bool _overrideColor;
        [SerializeField] private Color _color = Color.yellow;

        [Header("Override Scale")]
        [SerializeField] private bool _overrideScale;
        [SerializeField] private float _scale = 1.05f;

        [Header("Override Pulse Speed")]
        [SerializeField] private bool _overridePulseSpeed;
        [SerializeField] private float _pulseSpeed = 5.3f;

        [Header("Override On Time")]
        [SerializeField] private bool _overrideOnTime;
        [SerializeField] private float _onTime = 0.52f;

        [Header("Override Off Time")]
        [SerializeField] private bool _overrideOffTime;
        [SerializeField] private float _offTime = 0.25f;

        private readonly List<RuntimeOutlineMaterial> _runtimeMaterials = new List<RuntimeOutlineMaterial>();
        private Material _activeSourceMaterial;
        private bool _isOutlined;

        public void UpdateOutline(
            Transform player,
            Material defaultOutlineMaterial,
            float defaultRange,
            Color defaultColor,
            float defaultScale,
            float defaultPulseSpeed,
            float defaultOnTime,
            float defaultOffTime)
        {
            if (_target == null)
            {
                DisableOutline();
                return;
            }

            Material sourceMaterial = GetMaterial(defaultOutlineMaterial);

            if (sourceMaterial == null)
            {
                DisableOutline();
                Debug.LogWarning("No outline material assigned for " + _target.name);
                return;
            }

            float range = GetRange(defaultRange);
            bool playerInRange = _forceEnabled || Vector3.Distance(_target.transform.position, player.position) <= range;

            if (!playerInRange)
            {
                DisableOutline();
                return;
            }

            if (_isOutlined && _activeSourceMaterial != sourceMaterial)
            {
                DisableOutline();
            }

            if (!_isOutlined)
            {
                EnableOutline(sourceMaterial);
            }

            ApplySettings(defaultColor, defaultScale, defaultPulseSpeed, defaultOnTime, defaultOffTime);
        }

        public void DisableOutline()
        {
            if (!_isOutlined && _runtimeMaterials.Count == 0)
            {
                return;
            }

            for (int i = _runtimeMaterials.Count - 1; i >= 0; i--)
            {
                RuntimeOutlineMaterial runtimeMaterial = _runtimeMaterials[i];

                if (runtimeMaterial.Renderer != null)
                {
                    RemoveMaterial(runtimeMaterial.Renderer, runtimeMaterial.Material);
                }

                if (runtimeMaterial.Material != null)
                {
                    UnityEngine.Object.Destroy(runtimeMaterial.Material);
                }
            }

            _runtimeMaterials.Clear();
            _activeSourceMaterial = null;
            _isOutlined = false;
        }

        private void EnableOutline(Material sourceMaterial)
        {
            Renderer[] renderers = _target.GetComponentsInChildren<Renderer>(true);

            if (renderers.Length == 0)
            {
                Debug.LogWarning("No renderers found on " + _target.name);
                return;
            }

            for (int i = 0; i < renderers.Length; i++)
            {
                Renderer targetRenderer = renderers[i];
                Material runtimeMaterial = new Material(sourceMaterial);

                AppendMaterial(targetRenderer, runtimeMaterial);
                _runtimeMaterials.Add(new RuntimeOutlineMaterial(targetRenderer, runtimeMaterial));
            }

            _activeSourceMaterial = sourceMaterial;
            _isOutlined = true;
        }

        private void ApplySettings(
            Color defaultColor,
            float defaultScale,
            float defaultPulseSpeed,
            float defaultOnTime,
            float defaultOffTime)
        {
            Color effectiveColor = _overrideColor ? _color : defaultColor;
            float effectiveScale = _overrideScale ? _scale : defaultScale;
            float effectivePulseSpeed = _overridePulseSpeed ? _pulseSpeed : defaultPulseSpeed;
            float effectiveOnTime = _overrideOnTime ? _onTime : defaultOnTime;
            float effectiveOffTime = _overrideOffTime ? _offTime : defaultOffTime;

            for (int i = 0; i < _runtimeMaterials.Count; i++)
            {
                Material runtimeMaterial = _runtimeMaterials[i].Material;

                if (runtimeMaterial == null)
                {
                    continue;
                }

                runtimeMaterial.SetColor(ColorProperty, effectiveColor);
                runtimeMaterial.SetFloat(ScaleProperty, effectiveScale);
                runtimeMaterial.SetFloat(PulseSpeedProperty, effectivePulseSpeed);
                runtimeMaterial.SetFloat(OnTimeProperty, effectiveOnTime);
                runtimeMaterial.SetFloat(OffTimeProperty, effectiveOffTime);
            }
        }

        private Material GetMaterial(Material defaultOutlineMaterial)
        {
            return _overrideMaterial ? _outlineMaterial : defaultOutlineMaterial;
        }

        private float GetRange(float defaultRange)
        {
            return _overrideRange ? _range : defaultRange;
        }

        private void AppendMaterial(Renderer targetRenderer, Material materialToAppend)
        {
            Material[] currentMaterials = targetRenderer.sharedMaterials;
            Material[] updatedMaterials = new Material[currentMaterials.Length + 1];

            for (int i = 0; i < currentMaterials.Length; i++)
            {
                updatedMaterials[i] = currentMaterials[i];
            }

            updatedMaterials[updatedMaterials.Length - 1] = materialToAppend;
            targetRenderer.sharedMaterials = updatedMaterials;
        }

        private void RemoveMaterial(Renderer targetRenderer, Material materialToRemove)
        {
            Material[] currentMaterials = targetRenderer.sharedMaterials;
            List<Material> updatedMaterials = new List<Material>(currentMaterials.Length);

            for (int i = 0; i < currentMaterials.Length; i++)
            {
                if (currentMaterials[i] != materialToRemove)
                {
                    updatedMaterials.Add(currentMaterials[i]);
                }
            }

            targetRenderer.sharedMaterials = updatedMaterials.ToArray();
        }
    }

    private class RuntimeOutlineMaterial
    {
        public readonly Renderer Renderer;
        public readonly Material Material;

        public RuntimeOutlineMaterial(Renderer renderer, Material material)
        {
            Renderer = renderer;
            Material = material;
        }
    }
}
