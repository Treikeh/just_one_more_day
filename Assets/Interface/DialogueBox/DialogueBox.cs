using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// *This class is a bit messy and should be refactored. It does its job well, but there are parts that could be better
// // TODO: Find a way to send messages between the dialogue manager and ui without using code
// DONE: Find a better place to handle InputActionMap switching. I don't think the ui should be responsible for swithcing inputs
// // I could handle the InputActionMap switching in the DialogueManager, but connecting manager scripts together sonuds like a nightmare waiting to happen.

public class DialogueBox : MonoBehaviour
{
    // Time (in seconds) it takes for a new letter to appear
    [SerializeField] private Image characterPortrait;
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text dialogueSentence;
    [SerializeField] private Animator animator;
    [Range(0.8f, 1.4f)] [SerializeField] private float dialogueSoundMinPitch = 0.9f;
    [Range(0.8f, 1.4f)] [SerializeField] private float dialogueSoundMaxPitch = 1.1f;
    [SerializeField] private AudioSource dialogueSound;

    private bool endAutomatically = false;
    private float textSpeed;
    private int currentDialogue = 0;
    // Starts as -1 since it would skip over the first sentence if it was 0
    private int currentSentence = -1;
    private List<DialogueSO> dialogueList = new();
    // Reference to the DialogueTriggers dialogueFinishedEvent
    private UnityEvent dialogueTriggerEvent;
    private Coroutine sentenceAnimation;


    // Subscribe and unsubscribe from input events
    private void OnEnable() { InputManager.Instance.OnAdvancePressed += AdvanceDialogue; }
    private void OnDisable() { InputManager.Instance.OnAdvancePressed -= AdvanceDialogue; }


    // Advance sentence when pressing E
    private void AdvanceDialogue()
    {
        // Check if the sentece is still being animated and if so stop the animation and display the entire sentence
        if (sentenceAnimation != null)
        {
            dialogueSound.Stop();
            // Stop sentence animation
            StopCoroutine(sentenceAnimation);
            sentenceAnimation = null;
            // Display the entire sentence
            dialogueSentence.text = dialogueList[currentDialogue].sentences[currentSentence];
            if (endAutomatically)
            {
                DisplayNextSentence();
            }
        }
        else
        {
            DisplayNextSentence();
        }
    }


    public void StartDialogue(List<DialogueSO> list, UnityEvent dialogueEvent)
    {
        Debug.Log("Dialogue started");
        // Show dialogue window
        animator.SetBool("isOpen", true);
        // Reset values
        dialogueTriggerEvent = dialogueEvent;
        currentDialogue = 0;
        currentSentence = -1;
        dialogueList = list;
        // Show the first sentence in the dialogue list
        DisplayNextSentence();
    }

    // *Might be a good idea to change the name of this function so that it's clear that it starts the AnimateSentence coroutine
    public void DisplayNextSentence()
    {
        // Stop null reference error when there's no more dialogue and the player tries to advance the dialogue
        if (currentDialogue > dialogueList.Count - 1 || currentDialogue < 0)
        {
            Debug.Log("No More Dialogue");
            // Hide the window to stop player from being stuck in the dialogue window if they trigger a DialogueTrigger without any dialogue.
            // Should probably be a check in the StartDialogue function to not show the dialogue window if it recived no dialogue.
            StartCoroutine(ColseDialogueWindowDelay());
            return;
        }

        // Get the next sentence in the list
        currentSentence++;

        // Get the next dialogue object if we're past the last sentence in the current dialogue object
        if (currentSentence > dialogueList[currentDialogue].sentences.Count - 1)
        {
            currentSentence = 0;
            currentDialogue++;
            // End the dialogue if all of the dialogue has been displayed
            if(currentDialogue > dialogueList.Count - 1)
            {
                EndDialogue();
                return;
            }
        }
        UpdateUi(dialogueList[currentDialogue], currentSentence);
    }

    // *This function could do with a rewrite
    private void UpdateUi(DialogueSO dialogue, int sentence)
    {
        // Clear Text
        dialogueSentence.text = string.Empty;
        // Stop sentence animation
        StopAllCoroutines();

        // Start playing dialogue audio
        dialogueSound.Play();
        // Update display
        textSpeed = dialogue.textSpeed;
        endAutomatically = dialogue.endAutomatically;
        characterName.text = dialogue.characterName;
        characterPortrait.sprite = dialogue.characterPortrait;
        sentenceAnimation = StartCoroutine(AnimateSentence(dialogue.sentences[sentence]));

        // Send event to 
        UiManager.Instance.AddCharacterProfile(dialogue.characterProfile);
    }

    // Text "animation"
    private IEnumerator AnimateSentence(string sentence)
    {
        dialogueSentence.text = string.Empty;
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueSentence.text += letter;
            dialogueSound.pitch = Random.Range(dialogueSoundMinPitch, dialogueSoundMaxPitch);
            //dialogueSound.pitch = GetPitchByLetter(letter);
            //dialogueSound.Play();
            yield return new WaitForSeconds(textSpeed);
        }
        dialogueSound.Stop();
        // Remove reference to coroutine when it has finished
        sentenceAnimation = null;
        if (endAutomatically)
        {
            DisplayNextSentence();
        }
    }

    private float GetPitchByLetter(char letter)
    {
        return letter switch
        {
            'A' => 1f,
            'B' => 1f,
            'C' => 1f,
            'D' => 1f,
            'E' => 1f,
            'F' => 1f,
            'G' => 1f,
            'H' => 1f,
            'I' => 1f,
            'J' => 1f,
            'K' => 1f,
            'L' => 1f,
            'M' => 1f,
            'N' => 1f,
            'O' => 1f,
            'P' => 1f,
            'Q' => 1f,
            'R' => 1f,
            'S' => 1f,
            'T' => 1f,
            'U' => 1f,
            'V' => 1f,
            'W' => 1f,
            'X' => 1f,
            'Y' => 1f,
            'Z' => 1f,
            _ => 1,
        };
    }

    private void EndDialogue()
    {
        // Needs to be before "dialogueTriggerEvent?.Invoke();".
        // If it's after the "ColseDialogueWindowDelay" coroutine won't be stopped when starting a new dialogue
        // Which means that if you chain DialogueTriggers only the first DialogueTrigger will display all of it's sentences,
        // since the second DialogueTrigger will be cut off by the dialogue window closing.
        StartCoroutine(ColseDialogueWindowDelay());
        // This is it's own event because we only want to trigger the dialogueFinishedEvent on the DialogueTrigger that triggered this dialogue.
        // If we had connected the DialogueTrigger to dialogueFinished all DialogueTriggers would trigger their dialogueFinishedEvent
        // whenever any dialogue finished, which would cause an unknowable amount of errors.
        dialogueTriggerEvent?.Invoke();
    }

    // Have a small delay before hiding the dialogue window to DialogueTriggers to chain.
    // It's an IEnumerator because it gets stopped in the UpdateDialougeUi function.
    // Invoke(function, time) could also be used to hide it with a delay, but then we wouldn't be able to stop it from hiding when starting a new dialogue.
    private IEnumerator ColseDialogueWindowDelay()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("isOpen", false);
        // Another small delay for the animation to finish
        yield return new WaitForSeconds(0.2f);
        UiManager.Instance.DialogueFinished();
        Debug.Log("Dialogue finished");
    }
}
