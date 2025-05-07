using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// TODO: Make a way to limit how many times you can trigger the same dialogue and a way to change the dialogue to a "there's no more dialogue" dialogue.

public class DialogueStarter : MonoBehaviour, IInteractable
{
    public List<DialogueObject> dialogueList = new();
    public UnityEvent dialogueFinishedEvent;


    // Interact
    public void Interact()
    {
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        // Not sure it it's a good idea to Invoke an Action from this script or if i should call a function on the DialogueManager instead.
        // DialogueManager.startDialogue?.Invoke(dialogueList, dialogueFinishedEvent);
        UiManager.Instance.StartDialogue(dialogueList, dialogueFinishedEvent);
    }
}
