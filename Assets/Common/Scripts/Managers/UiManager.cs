using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class UiManager : MonoBehaviour
{
    // Create singleton instance
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


    public event Action<List<DialogueObject>, UnityEvent> onDialogueStarted;
    public void startDialogue(List<DialogueObject> lsit, UnityEvent @event)
    {
        onDialogueStarted?.Invoke(lsit, @event);
    }

    public event Action onDialogueFinished;
    public void DialogueFinished()
    {
        onDialogueFinished?.Invoke();
    }
}
