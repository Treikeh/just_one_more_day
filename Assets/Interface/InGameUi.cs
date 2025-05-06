using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InGameUi : MonoBehaviour
{
    [SerializeField] private JournalUi journal;
    [SerializeField] private DialogueBox dialogueBox;


    private void OnEnable()
    {
        GameEventManager.Instance.inputEvents.onJournalPressed += JounralPressed;
        GameEventManager.Instance.inputEvents.onCancelPressed += CancelPressed;

        GameEventManager.Instance.uiEvents.onDialogueStarted += DialogueStarted;
        GameEventManager.Instance.uiEvents.onDialogueFinished += DialogueFinished;
    }

    private void OnDisable()
    {
        GameEventManager.Instance.inputEvents.onJournalPressed -= JounralPressed;
        GameEventManager.Instance.inputEvents.onCancelPressed -= CancelPressed;

        GameEventManager.Instance.uiEvents.onDialogueStarted -= DialogueStarted;
        GameEventManager.Instance.uiEvents.onDialogueFinished -= DialogueFinished;
    }


    private void JounralPressed()
    {
        if (!journal.gameObject.activeInHierarchy)
        {
            journal.gameObject.SetActive(true);
            GameEventManager.Instance.inputEvents.ActionMapChanged("Ui");
        }
    }

    // This function is public so that i can activate it with ui buttons
    private void CancelPressed()
    {
        if (journal.gameObject.activeInHierarchy)
        {
            journal.gameObject.SetActive(false);
            GameEventManager.Instance.inputEvents.ActionMapChanged("Player");
        }
    }

    private void DialogueStarted(List<DialogueObject> list, UnityEvent @event)
    {
        dialogueBox.gameObject.SetActive(true);
        dialogueBox.StartDialogue(list, @event);
        GameEventManager.Instance.inputEvents.ActionMapChanged("Ui");
    }

    private void DialogueFinished()
    {
        dialogueBox.gameObject.SetActive(false);
        GameEventManager.Instance.inputEvents.ActionMapChanged("Player");
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
