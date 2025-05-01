using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// TODO: Make a way to limit how many times you can trigger the same dialogue

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    public List<DialogueObject> dialogueList = new();
    public UnityEvent dialogueFinishedEvent;


    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogueList, dialogueFinishedEvent);
    }


    // Interact
    public void Interact()
    {
        TriggerDialogue();
    }

    public bool CanInteract()
    {
        return true;
    }
}
