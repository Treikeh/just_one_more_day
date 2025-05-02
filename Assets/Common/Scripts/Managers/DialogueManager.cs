using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Don't like using MonoBehaviour manager classes since it means you need to drag and drop it into every scene where you use it's functionality.
// Unfortunately it works very well, so here i am.
// TODO: Find a way to load manager scripts automatically when running game in editor

public class DialogueManager : MonoBehaviour
{
    // Create singleton instance
    public static DialogueManager Instance {get; private set;}
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }


    // Not the biggest fan of this UnityAction, but it works so i'm going to leave it alone for now - TH
    public UnityAction<DialogueObject, int> updateUi;
    public UnityAction dialogueStarted;
    public UnityAction dialogueFinished;

    private int currentDialogue = 0;
    // Starts as -1 since it would skip over the first sentence if it was 0
    private int currentSentence = -1;
    private List<DialogueObject> dialogueList;
    private UnityEvent dialogueTriggerEvent;


    public void StartDialogue(List<DialogueObject> list, UnityEvent dialogueEvent)
    {
        // Reset values
        dialogueTriggerEvent = dialogueEvent;
        currentDialogue = 0;
        currentSentence = -1;
        dialogueList = list;
        dialogueStarted?.Invoke();
        GetNextSentence();
    }

    // Should move the updateUi action out of this function or rename the function to be more descriptive of what it actually does.
    public void GetNextSentence()
    {
        // !WHY DID I DO THIS!?!?!?!?!
        // Stop null reference error when there's no more dialogue
        if (currentDialogue > dialogueList.Count - 1)
        {
            Debug.Log("No More Dialogue");
            return;
        }
        // Get the next sentence in the list
        currentSentence++;

        if(currentSentence > dialogueList[currentDialogue].sentences.Count - 1)
        {
            currentSentence = 0;
            currentDialogue++;
            if(currentDialogue > dialogueList.Count - 1)
            {
                Debug.Log("End of Dialogue");
                dialogueFinished?.Invoke();
                // This is it's own event because we only want to trigger the dialogueFinishedEvent on the DialogueTrigger that triggered this dialogue.
                // If we had connected the DialogueTrigger to dialogueFinished all DialogueTriggers would trigger their dialogueFinishedEvent
                // whenever any dialogue finished, which would cause an unknowable amount of errors.
                dialogueTriggerEvent?.Invoke();
                return;
            }
        }
        updateUi?.Invoke(dialogueList[currentDialogue], currentSentence);
    }
}
