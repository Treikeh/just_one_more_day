using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class UiManager : MonoBehaviour
{
    public event Action<List<DialogueSO>, UnityEvent> OnDialogueStarted;
    public event Action OnDialogueFinished;

    public static UiManager Instance { get; private set; }
    [SerializeField] private GameObject inGameUi;

    private List<CharacterProfileSO> characterProfiles = new();


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        // Spawn InGameUi prefab and add it to don't destroy on load
        DontDestroyOnLoad(Instantiate(inGameUi));
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
    }

    public List<CharacterProfileSO> GetCharacterProfiles()
    {
        return characterProfiles;
    }
}
