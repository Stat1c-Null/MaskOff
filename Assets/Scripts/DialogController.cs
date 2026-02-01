using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System;
using System.Collections.Generic;

public class DialogController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject clickerIcon;

    [Header("Dialog Data")]
    [SerializeField] private List<DialogEntry> dialogEntries = new List<DialogEntry>();

    [Header("End Dialog Settings")]
    [SerializeField] private EndDialogAction endAction = EndDialogAction.HideDialog;
    [SerializeField] private string nextSceneName;

    [Header("Input")]
    [SerializeField] private InputActionReference clickAction;

    private int currentDialogIndex = 0;
    private bool isDialogActive = false;

    // Static property that other scripts can check
    public static bool IsGamePaused { get; private set; } = false;

    public enum EndDialogAction
    {
        HideDialog,
        LoadNextScene
    }

    [Serializable]
    public class DialogEntry
    {
        public string speakerName;
        [TextArea(3, 5)]
        public string dialogText;
    }

    private void Awake()
    {
        // Pause the game
        Time.timeScale = 0f;
    }

    private void OnEnable()
    {
        // Auto-start dialog when this object becomes active
        if (dialogEntries.Count == 0)
        {
            Debug.LogWarning("No dialog entries to display!");
            return;
        }

        currentDialogIndex = 0;
        isDialogActive = true;

        // Pause the game
        Time.timeScale = 0f;
        IsGamePaused = true;
        Debug.Log("DialogController: Game PAUSED - TimeScale set to " + Time.timeScale);

        // Subscribe to input action
        if (clickAction != null)
        {
            clickAction.action.Enable();
            clickAction.action.performed += OnClickPerformed;
        }

        DisplayCurrentDialog();
    }

    private void OnDisable()
    {
        // Unsubscribe from input action
        if (clickAction != null)
        {
            clickAction.action.performed -= OnClickPerformed;
        }
    }

    private void OnClickPerformed(InputAction.CallbackContext context)
    {
        if (isDialogActive)
        {
            ShowNextDialog();
        }
    }

    private void ShowNextDialog()
    {
        currentDialogIndex++;

        if (currentDialogIndex >= dialogEntries.Count)
        {
            EndDialog();
        }
        else
        {
            DisplayCurrentDialog();
        }
    }

    private void DisplayCurrentDialog()
    {
        if (currentDialogIndex < dialogEntries.Count)
        {
            DialogEntry entry = dialogEntries[currentDialogIndex];

            if (nameText != null)
                nameText.text = entry.speakerName;

            if (dialogText != null)
                dialogText.text = entry.dialogText;
        }
    }

    private void EndDialog()
    {
        isDialogActive = false;

        // Unpause the game
        Time.timeScale = 1f;
        IsGamePaused = false;
        Debug.Log("DialogController: Game UNPAUSED - TimeScale set to " + Time.timeScale);

        // Handle end action
        switch (endAction)
        {
            case EndDialogAction.HideDialog:
                // Dialog is already hidden, nothing more to do
                dialogBox.SetActive(false);
                break;

            case EndDialogAction.LoadNextScene:
                if (!string.IsNullOrEmpty(nextSceneName))
                {
                    SceneManager.LoadScene(nextSceneName);
                }
                else
                {
                    Debug.LogWarning("Next scene name is not specified!");
                }
                break;
        }
    }

    /// <summary>
    /// Add a dialog entry at runtime
    /// </summary>
    public void AddDialogEntry(string speaker, string text)
    {
        dialogEntries.Add(new DialogEntry
        {
            speakerName = speaker,
            dialogText = text
        });
    }

    /// <summary>
    /// Clear all dialog entries
    /// </summary>
    public void ClearDialogEntries()
    {
        dialogEntries.Clear();
    }

    /// <summary>
    /// Set what happens when dialog ends
    /// </summary>
    public void SetEndAction(EndDialogAction action, string sceneName = "")
    {
        endAction = action;
        nextSceneName = sceneName;
    }

    /// <summary>
    /// Check if dialog is currently active
    /// </summary>
    public bool IsDialogActive()
    {
        return isDialogActive;
    }

    /// <summary>
    /// Force skip to end of dialog
    /// </summary>
    public void SkipDialog()
    {
        if (isDialogActive)
        {
            EndDialog();
        }
    }
}
