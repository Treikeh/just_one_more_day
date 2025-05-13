using TMPro;
using UnityEngine;

public class CharactersPage : MonoBehaviour
{
    [SerializeField] private TMP_Text characterNameText;
    [SerializeField] private TMP_Text characterDescriptionText;


    private void OnEnable()
    {
        // Display character profiles
        foreach (CharacterProfileSO profile in UiManager.Instance.GetCharacterProfiles())
        {
            characterNameText.text = $"{profile.characterName}";
            characterDescriptionText.text = $"{profile.characterDescription}";
        }
    }
}
