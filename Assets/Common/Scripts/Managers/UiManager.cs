using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class UiManager : MonoBehaviour
{
    public event Action<List<DialogueSO>, UnityEvent> OnDialogueStarted;
    public event Action OnDialogueFinished;
    public event Action OnJournalNotification;
    public event Action OnJounralNotificationSeen;

    public static UiManager Instance { get; private set; }
    public bool hudActive = false;
    public List<CharacterProfileSO> characterProfiles = new();

    [SerializeField] private GameObject inGameUiPrefab;
    private GameObject inGameUi;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        // Spawn InGameUi prefab and add it to don't destroy on load
        inGameUi = Instantiate(inGameUiPrefab);
        DontDestroyOnLoad(inGameUi);
    }


    public void HideInGameUi()
    {
        inGameUi.SetActive(false);
    }

    public void ShowInGameUi()
    {
        inGameUi.SetActive(true);
    }


    public void StartDialogue(List<DialogueSO> lsit, UnityEvent @event)
    {
        OnDialogueStarted?.Invoke(lsit, @event);
    }

    public void DialogueFinished()
    {
        OnDialogueFinished?.Invoke();
    }

    public void AddCharacterProfile(CharacterProfileSO profile)
    {
        if (characterProfiles.Contains(profile))
        {
            return;
        }
        characterProfiles.Add(profile);
        EmitJournalNotification();
    }

    public void EmitJournalNotification()
    {
        OnJournalNotification?.Invoke();
    }

    public void EmitJournalNotificationSeen()
    {
        OnJounralNotificationSeen?.Invoke();
    }

    public List<CharacterProfileSO> GetCharacterProfiles()
    {
        return characterProfiles;
    }
}
