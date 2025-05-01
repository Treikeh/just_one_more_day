using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUi : MonoBehaviour
{
    // TODO: Find a way to send messages between the dialogue manager and ui without using code
    // Time (in seconds) it takes for a new letter to appear
    [SerializeField] private float textSpeed = 0.05f;
    [SerializeField] private Image characterPortrait;
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text dialogueSentence;
    [SerializeField] private Animator animator;


    private void OnEnable()
    {
        DialogueManager.Instance.updateUi += UpdateDialougeUi;
        DialogueManager.Instance.dialogueFinished += EndDialogue;
    }

    private void OnDisable()
    {
        DialogueManager.Instance.updateUi -= UpdateDialougeUi;
        DialogueManager.Instance.dialogueFinished -= EndDialogue;
    }

    private void UpdateDialougeUi(DialogueObject dialogue, int sentence)
    {
        animator.SetBool("IsOpen", true);
        // Clear Text
        dialogueSentence.text = string.Empty;
        StopAllCoroutines();

        // Update display
        characterName.text = dialogue.characterName;
        characterPortrait.sprite = dialogue.characterPortrait;
        StartCoroutine(TypeSentence(dialogue.sentences[sentence]));
    }

    private void EndDialogue()
    {
        StartCoroutine(ColseDialogueWindowDelay());
    }


    public void ShowNextSentence()
    {
        DialogueManager.Instance.GetNextSentence();
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueSentence.text = string.Empty;
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueSentence.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    // Have a small delay before hiding the dialogue window in case the DialogueTriggers dialogueFinishedEvent tiggers another DialogueTrigger.
    // It's an IEnumerator because it gets stopped in the UpdateDialougeUi function.
    // Invoke(function, time) could also be used to hide it with a delay, but then we wouldn't be able to stop it from hiding when starting a new dialogue.
    private IEnumerator ColseDialogueWindowDelay()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("IsOpen", false);
    }
}
