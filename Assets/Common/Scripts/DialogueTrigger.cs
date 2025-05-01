using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public List<DialogueObject> dialogueList = new();

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogueList);
    }
}
