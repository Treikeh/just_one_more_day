using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// TODO: Find a way to send messages between the dialogue manager and ui without using code
// TODO: Find a better place to handle InputActionMap switching. The ui SHOULD NOT be responsible for swithcing inputs (I belive)
// I could handle the InputActionMap switching in the DialogueManager, but connecting manager scripts together sonuds like a nightmare waiting to happen.

public class DialogueBox : MonoBehaviour
{
    // Time (in seconds) it takes for a new letter to appear
    [SerializeField] private float textSpeed = 0.05f;
    [SerializeField] private Image characterPortrait;
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text dialogueSentence;
    [SerializeField] private Animator animator;


    // Subscribe to events
    private void OnEnable()
    {
        DialogueManager.Instance.updateUi += UpdateDialougeUi;
        DialogueManager.Instance.dialogueFinished += EndDialogue;
        InputManager.Instance.inputActions.Ui.Advance.performed += ShowNextSentence;
    }

    // Unsubscribe from events
    private void OnDisable()
    {
        DialogueManager.Instance.updateUi -= UpdateDialougeUi;
        DialogueManager.Instance.dialogueFinished -= EndDialogue;
        InputManager.Instance.inputActions.Ui.Advance.performed -= ShowNextSentence;
    }

    private void UpdateDialougeUi(DialogueObject dialogue, int sentence)
    {
        animator.SetBool("IsOpen", true);
        InputManager.Instance.ToggleActionMap(InputManager.Instance.inputActions.Ui);
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


    public void ShowNextSentence(InputAction.CallbackContext context)
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
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("IsOpen", false);
        InputManager.Instance.ToggleActionMap(InputManager.Instance.inputActions.Player);
    }
}
