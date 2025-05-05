using System;
using System.Collections.Generic;
using UnityEngine.Events;

public class DialogueEvents
{
    public event Action<List<DialogueObject>, UnityEvent> onDialogueStarted;
    public void DialogueStarted(List<DialogueObject> dialogueList, UnityEvent dialogueFinishEvent)
    {
        if (onDialogueStarted != null)
        {
            onDialogueStarted(dialogueList, dialogueFinishEvent);
        }
    }

    public event Action onDialogueFinished;
    public void DialogueFinished()
    {
        if (onDialogueFinished != null)
        {
            onDialogueFinished();
        }
    }
}
