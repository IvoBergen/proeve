using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class NoteUI : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent Finished;

    [Header("UI Elements")]
    [SerializeField] private GameObject _counterParent;
    [SerializeField] private TextMeshProUGUI _counterText;

    [Header("Photo Preview")]
    [SerializeField] private GameObject _previewPanel;
    [SerializeField] private Image _previewDisplay;

    [Header("Settings")]
    [SerializeField] private int _totalPapers = 4;

    private int _papersFound = 0;
    private bool _isQuestActive = false;
    private bool _isViewingPhoto = false;

    /// <summary>
    /// Gets the current status of the quest.
    /// </summary>
    public bool IsQuestActive => _isQuestActive;

    private void Start()
    {
        // Remove debug logs after testing
        if (_counterParent != null) _counterParent.SetActive(false);
        if (_previewPanel != null) _previewPanel.SetActive(false);
    }

    /// <summary>
    /// Activates the quest and shows the initial UI counter.
    /// </summary>
    public void StartQuest()
    {
        _isQuestActive = true;
        if (_counterParent != null) _counterParent.SetActive(true);
        UpdateCounterUI();
    }

    private void Update()
    {
        // Close the image with Space, Click, or E
        if (_isViewingPhoto)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
            {
                ClosePhoto();
            }
        }
    }

    /// <summary>
    /// Handles the logic when a paper is collected, including updating UI and showing the image.
    /// </summary>
    /// <param name="note">The collected Note object containing images.</param>
    public void OnPaperCollected(Notes note)
    {
        _papersFound++;
        UpdateCounterUI();

        // Choose the correct image based on the counter
        Sprite spriteToShow = (_papersFound >= _totalPapers) ? note.clueImage : note.normalImage;

        OpenPhoto(spriteToShow);

        if (_papersFound >= _totalPapers)
        {
            // Use appropriate functionalities: delay hiding the UI
            Invoke("HideCounter", 2f);
            Finished?.Invoke();
        }

        Destroy(note.gameObject);
    }

    private void OpenPhoto(Sprite photo)
    {
        if (_previewDisplay != null && photo != null)
        {
            _previewDisplay.sprite = photo;
            _previewPanel.SetActive(true);
            _isViewingPhoto = true;

            // Pause the game so the player can look at the photo
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void ClosePhoto()
    {
        _previewPanel.SetActive(false);
        _isViewingPhoto = false;

        // Resume game
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UpdateCounterUI()
    {
        if (_counterText != null)
        {
            _counterText.text = "Papiertjes: " + _papersFound + " / " + _totalPapers;
        }
    }

    private void HideCounter()
    {
        if (_counterParent != null)
        {
            _counterParent.SetActive(false);
        }
    }
}