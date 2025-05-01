using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public UnityAction<DialogueObject, int> updateUi;
    public UnityAction dialogueFinished;

    public static DialogueManager Instance {get; private set;}
    // Set static instance
    private void Awake()
    {
        // If instance allready exists destroy this object
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


    private List<DialogueObject> dialogueList;
    private int currentDialogue = 0;
    // Starts as -1 since it would skip over the first sentence if it was 0
    private int currentSentence = -1;

    private UnityEvent dialogueTriggerEvent;

    public void StartDialogue(List<DialogueObject> list, UnityEvent dialogueEvent)
    {
        // Reset values
        dialogueTriggerEvent = dialogueEvent;
        currentDialogue = 0;
        currentSentence = -1;
        dialogueList = list;
        GetNextSentence();
    }

    public void GetNextSentence()
    {
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
