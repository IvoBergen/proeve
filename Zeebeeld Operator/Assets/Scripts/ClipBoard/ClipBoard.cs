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

    private void Awake()
    {
        if (_clueManager == null)
        {
            _clueManager = ClueManager.Instance;
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

    private void OnDisable()
    {
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
}
