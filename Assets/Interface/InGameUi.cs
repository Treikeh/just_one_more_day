using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InGameUi : MonoBehaviour
{
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject journal;
    [SerializeField] private DialogueBox dialogueBox;

    private bool inDialogue = false;


    private void OnEnable()
    {
        InputManager.Instance.OnJournalPressed += JounralPressed;
        InputManager.Instance.OnCancelPressed += CancelPressed;

        UiManager.Instance.OnDialogueStarted += DialogueStarted;
        UiManager.Instance.OnDialogueFinished += DialogueFinished;

        SaveManager.GameLoaded += OnGameLoaded;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnJournalPressed -= JounralPressed;
        InputManager.Instance.OnCancelPressed -= CancelPressed;

        UiManager.Instance.OnDialogueStarted -= DialogueStarted;
        UiManager.Instance.OnDialogueFinished -= DialogueFinished;

        SaveManager.GameLoaded -= OnGameLoaded;
    }

    private void Start()
    {
        if (QuestManager.Instance.hasJournal)
        {
            ShowHud();
        }
    }

    private void OnGameLoaded()
    {
        Invoke(nameof(Start), 0.5f);
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

    public void OnMainMenuButtonPressed()
    {
        CancelPressed();
        QuestManager.Instance.Reset();
        LevelManager.Instance.Reset();
        LevelManager.Instance.StartLoadingLevel("MainMenu");
        HideHud();
    }

    public void OnSaveGameButtonPressed()
    {
        SaveManager.SaveGame();
    }


    // HUD
    public void ActivateHud(bool active)
    {
        ShowHud();
        QuestManager.Instance.hasJournal = active;
    }

    public void ShowHud()
    {
        hud.GetComponent<Animator>().SetBool("isOpen", true);
    }

    public void HideHud()
    {
        hud.GetComponent<Animator>().SetBool("isOpen", false);
    }


    // JOURNAL
    private void JounralPressed()
    {
        if (!journal.activeInHierarchy && QuestManager.Instance.hasJournal && !inDialogue)
        {
            journal.SetActive(true);
            journal.GetComponent<Animator>().SetBool("isOpen", true);
            InputManager.Instance.ChangeActionMap("Ui");
            UiManager.Instance.EmitJournalNotificationSeen();
            HideHud();
        }
    }

    // This function is public so that i can activate it with ui buttons
    private void CancelPressed()
    {
        if (journal.activeInHierarchy && QuestManager.Instance.hasJournal)
        {
            journal.GetComponent<Animator>().SetBool("isOpen", false);
            // Set player input if not in dialogue
            if (!inDialogue)
            {
                InputManager.Instance.ChangeActionMap("Player");
            }
            StartCoroutine(CloseJournalDelay());
            ShowHud();
        }
    }

    private IEnumerator CloseJournalDelay()
    {
        yield return new WaitForSeconds(0.25f);
        journal.SetActive(false);
    }


    // DIALOGUE
    private void DialogueStarted(List<DialogueSO> list, UnityEvent @event)
    {
        dialogueBox.gameObject.SetActive(true);
        dialogueBox.StartDialogue(list, @event);
        InputManager.Instance.ChangeActionMap("Ui");
        inDialogue = true;
    }

    private void DialogueFinished()
    {
        dialogueBox.gameObject.SetActive(false);
        InputManager.Instance.ChangeActionMap("Player");
        inDialogue = false;
    }
}
