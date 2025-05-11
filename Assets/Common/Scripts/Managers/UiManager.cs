using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class UiManager : MonoBehaviour
{
    public event Action<List<DialogueSO>, UnityEvent> OnDialogueStarted;
    public event Action OnDialogueFinished;

    public static UiManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }


    public void StartDialogue(List<DialogueSO> lsit, UnityEvent @event)
    {
        OnDialogueStarted?.Invoke(lsit, @event);
    }

    public void DialogueFinished()
    {
        OnDialogueFinished?.Invoke();
    }
}
