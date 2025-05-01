using System.Collections;
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


    private void OnEnable()
    {
        DialogueManager.updateUi += UpdateDialougeUi;
    }

    private void OnDisable()
    {
        DialogueManager.updateUi -= UpdateDialougeUi;
    }


    public void UpdateDialougeUi(DialogueObject dialogue, int sentence)
    {
        // Clear Text
        dialogueSentence.text = string.Empty;
        StopAllCoroutines();

        // Update display
        characterName.text = dialogue.characterName;
        characterPortrait.sprite = dialogue.characterPortrait;
        StartCoroutine(TypeSentence(dialogue.sentences[sentence]));
    }

    public void TriggerNextSentence()
    {
        DialogueManager.Instance.GetNexSentence();
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
}
