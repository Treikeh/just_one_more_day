using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InGameUi : MonoBehaviour
{
    [SerializeField] private GameObject journal;
    [SerializeField] private DialogueBox dialogueBox;


    private void OnEnable()
    {
        InputManager.Instance.OnJournalPressed += JounralPressed;
        InputManager.Instance.onCancelPressed += CancelPressed;

        UiManager.Instance.OnDialogueStarted += DialogueStarted;
        UiManager.Instance.OnDialogueFinished += DialogueFinished;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnJournalPressed -= JounralPressed;
        InputManager.Instance.onCancelPressed -= CancelPressed;

        UiManager.Instance.OnDialogueStarted -= DialogueStarted;
        UiManager.Instance.OnDialogueFinished -= DialogueFinished;
    }


    // INPUTS
    public void OnResumeButtonPressed()
    {
        CancelPressed();
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }


    // JOURNAL
    private void JounralPressed()
    {
        if (!journal.activeInHierarchy)
        {
            journal.SetActive(true);
            InputManager.Instance.ChangeActionMap("Ui");
        }
    }

    // This function is public so that i can activate it with ui buttons
    private void CancelPressed()
    {
        if (journal.activeInHierarchy)
        {
            journal.SetActive(false);
            InputManager.Instance.ChangeActionMap("Player");
        }
    }


    // DIALOGUE
    private void DialogueStarted(List<DialogueSO> list, UnityEvent @event)
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
}
