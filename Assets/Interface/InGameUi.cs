using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InGameUi : MonoBehaviour
{
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject journal;
    [SerializeField] private DialogueBox dialogueBox;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip openJournalClip;
    [SerializeField] private AudioClip closeJournalClip;

    private bool inDialogue = false;


    private void OnEnable()
    {
        InputManager.Instance.OnJournalPressed += JounralPressed;
        InputManager.Instance.OnCancelPressed += CancelPressed;

        UiManager.Instance.OnDialogueStarted += DialogueStarted;
        UiManager.Instance.OnDialogueFinished += DialogueFinished;

        SaveManager.GameLoaded += OnGameLoaded;

        OnGameLoaded();
    }

    private void OnDisable()
    {
        InputManager.Instance.OnJournalPressed -= JounralPressed;
        InputManager.Instance.OnCancelPressed -= CancelPressed;

        UiManager.Instance.OnDialogueStarted -= DialogueStarted;
        UiManager.Instance.OnDialogueFinished -= DialogueFinished;

        SaveManager.GameLoaded -= OnGameLoaded;
    }


    private void OnGameLoaded()
    {
        Invoke(nameof(WhoCares), 0.5f);
    }


    private void WhoCares()
    {
        if (QuestManager.Instance.hasJournal)
        {
            ShowHud();
        }
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
        Utils.ShowMouseCursor();
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
            audioSource.PlayOneShot(openJournalClip);
            journal.SetActive(true);
            journal.GetComponent<Animator>().SetBool("isOpen", true);
            Utils.ShowMouseCursor();
            InputManager.Instance.ChangeActionMap("Ui");
            UiManager.Instance.EmitJournalNotificationSeen();
            HideHud();
        }
    }

    private void CancelPressed()
    {
        audioSource.PlayOneShot(closeJournalClip);
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

    // Small delay to allow the animatinon to finish before disabling Journal
    private IEnumerator CloseJournalDelay()
    {
        yield return new WaitForSeconds(0.25f);
        Utils.HideMouseCursor();
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
