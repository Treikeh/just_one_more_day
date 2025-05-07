using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InGameUi : MonoBehaviour
{
    [SerializeField] private JournalUi journal;
    [SerializeField] private DialogueBox dialogueBox;


    private void OnEnable()
    {
        InputManager.Instance.onJournalPressed += JounralPressed;
        InputManager.Instance.onCancelPressed += CancelPressed;

        UiManager.Instance.onDialogueStarted += DialogueStarted;
        UiManager.Instance.onDialogueFinished += DialogueFinished;
    }

    private void OnDisable()
    {
        InputManager.Instance.onJournalPressed -= JounralPressed;
        InputManager.Instance.onCancelPressed -= CancelPressed;

        UiManager.Instance.onDialogueStarted -= DialogueStarted;
        UiManager.Instance.onDialogueFinished -= DialogueFinished;
    }


    private void JounralPressed()
    {
        if (!journal.gameObject.activeInHierarchy)
        {
            journal.gameObject.SetActive(true);
            InputManager.Instance.ChangeActionMap("Ui");
        }
    }

    // This function is public so that i can activate it with ui buttons
    private void CancelPressed()
    {
        if (journal.gameObject.activeInHierarchy)
        {
            journal.gameObject.SetActive(false);
            InputManager.Instance.ChangeActionMap("Player");
        }
    }

    private void DialogueStarted(List<DialogueObject> list, UnityEvent @event)
    {
        dialogueBox.gameObject.SetActive(true);
        dialogueBox.StartDialogue(list, @event);
        InputManager.Instance.ChangeActionMap("Ui");
    }

    private void DialogueFinished()
    {
        dialogueBox.gameObject.SetActive(false);
        InputManager.Instance.ChangeActionMap("Player");
    }


    public void OnResumeButtonPressed()
    {
        CancelPressed();
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }
}
