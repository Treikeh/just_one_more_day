using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharactersPage : MonoBehaviour
{
    [SerializeField] private TMP_Text characterNameText;
    [SerializeField] private TMP_Text characterDescriptionText;
    [SerializeField] private Image characterPortrait;

    private int currentCharacterIndex = 0;
    private List<CharacterProfileSO> profiles;


    private void OnEnable()
    {
        profiles = UiManager.Instance.GetCharacterProfiles();
        if (profiles.Count > 0)
        {
            UpdatePage(profiles[currentCharacterIndex]);
        }
    }

    private void UpdatePage(CharacterProfileSO profile)
    {
        characterNameText.text = $"{profile.characterName}";
        characterDescriptionText.text = $"{profile.characterDescription}";
        characterPortrait.sprite = profile.characterPortrait;
    }

    public void DisplayNextCharacter()
    {
        Debug.Log("Next Character");
        currentCharacterIndex++;
        if (currentCharacterIndex > profiles.Count - 1)
        {
            currentCharacterIndex = 0;
        }

        UpdatePage(profiles[currentCharacterIndex]);
    }

    public void DisplayPreviousCharacter()
    {
        Debug.Log("Previous Character");
        currentCharacterIndex--;
        if (currentCharacterIndex < 0)
        {
            currentCharacterIndex = profiles.Count - 1;
        }
        
        UpdatePage(profiles[currentCharacterIndex]);
    }
}
