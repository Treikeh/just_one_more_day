using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    // Parameters: UnityAction<DialogueObject currentDialogue, int currentSentence>
    public static UnityAction<DialogueObject, int> updateUi;
    public static DialogueManager Instance {get; private set;}
    // Set static instance
    private void Awake()
    {
        // If instance allready exists destroy this object
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    private List<DialogueObject> dialogues = new();
    private int currentDialogue = 0;
    // Starts as -1 since it would skip over the first sentence if it was 0
    private int currentSentence = -1;

    public void StartDialogue(List<DialogueObject> dialoguesList)
    {
        currentDialogue = 0;
        currentSentence = -1;
        dialogues = dialoguesList;
        GetNexSentence();
    }

    // Goes through every sentence in every dialogue object before ending the 
    public void GetNexSentence()
    {
        // Get the next sentence in the list
        currentSentence++;

        if(currentSentence > dialogues[currentDialogue].sentences.Count - 1)
        {
            currentSentence = 0;
            currentDialogue++;
            if(currentDialogue > dialogues.Count - 1)
            {
                Debug.Log("End of Dialogue");
                return;
            }
        }

        updateUi?.Invoke(dialogues[currentDialogue], currentSentence);
    }
}
