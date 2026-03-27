using bnyhtz;
using System.Text;
using TMPro;
using UnityEngine;

/// <summary>
/// xxx
/// </summary>
public class Clipboard : MonoBehaviour
{
    [SerializeField] private ClueManager _clueManager;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameStateManager _gameStateManager;
    [SerializeField] private DialogueManager _dialogueManager;
    [SerializeField] private KeyCode _toggleKey = KeyCode.Tab;
    [SerializeField] private bool _startOpen = false;
    private GameObject _clipboardVisualRoot;

    private bool _isOpen;

    private void Awake()
    {
        _clipboardVisualRoot = gameObject;

        if (_clueManager == null)
        {
            _clueManager = ClueManager.Instance;
        }

        if (_gameStateManager == null)
        {
            _gameStateManager = FindObjectOfType<GameStateManager>();
        }

        if (_dialogueManager == null)
        {
            _dialogueManager = FindObjectOfType<DialogueManager>();
        }
    }

    private void OnEnable()
    {
        if (_clueManager == null)
        {
            _clueManager = ClueManager.Instance;
        }

        if (_clueManager != null)
        {
            _clueManager.OnCluesChanged += RefreshText;
        }

        RefreshText();
    }

    private void Start()
    {
        SetClipboardState(_startOpen, true);
    }

    private void Update()
    {
        if (!Input.GetKeyDown(_toggleKey))
        {
            return;
        }

        bool wantsToOpen = !_isOpen;
        if (wantsToOpen && _dialogueManager != null && _dialogueManager.Active)
        {
            return;
        }

        SetClipboardState(wantsToOpen, false);
    }

    private void OnDisable()
    {
        if (_isOpen)
        {
            SetClipboardState(false, false);
        }

        if (_clueManager != null)
        {
            _clueManager.OnCluesChanged -= RefreshText;
        }
    }

    private void RefreshText()
    {
        if (_text == null)
        {
            return;
        }

        if (_clueManager == null)
        {
            _text.text = string.Empty;
            return;
        }

        var clues = _clueManager.GetAllClues();
        if (clues == null || clues.Count == 0)
        {
            _text.text = string.Empty;
            return;
        }

        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < clues.Count; i++)
        {
            if (i > 0)
            {
                builder.AppendLine();
            }

            builder.Append("- ");
            builder.Append(clues[i]?.clueText ?? string.Empty);
        }

        _text.text = builder.ToString();
    }

    private void SetClipboardState(bool open, bool force)
    {
        if (!force && _isOpen == open)
        {
            return;
        }

        _isOpen = open;

        if (_clipboardVisualRoot != null && _clipboardVisualRoot != gameObject)
        {
            _clipboardVisualRoot.SetActive(open);
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(open);
            }
        }

        if (_gameStateManager != null)
        {
            if (open)
            {
                _gameStateManager.OpenClipboard();
            }
            else
            {
                _gameStateManager.CloseClipboard();
            }
        }
    }
}
