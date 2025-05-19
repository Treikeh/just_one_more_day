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
    private List<CharacterProfileSO> profiles = new();


    private void OnEnable()
    {
        // Get all character profiles from UiManager
        profiles = UiManager.Instance.GetCharacterProfiles();
        // If there are any profiles display the profile that mathces the current index (First profile when opening the jounral for the first time).
        if (profiles.Count > 0)
        {
            UpdatePage(profiles[currentCharacterIndex]);
        }
    }

    private void UpdatePage(CharacterProfileSO profile)
    {
        // Set Ui elements to profile data
        characterNameText.text = $"{profile.characterName}";
        characterDescriptionText.text = $"{profile.characterDescription}";
        characterPortrait.sprite = profile.characterPortrait;
    }

    public void DisplayNextCharacter()
    {
        // Stop function when there are no profiles
        if (profiles.Count <= 0) { return; }

        // Increase the current index
        currentCharacterIndex++;
        // Check if the current index is greater than the last profile index and if it is set it to zero
        // This makes the palyer go to the first page when they try to go past the last page
        if (currentCharacterIndex > profiles.Count - 1)
        {
            currentCharacterIndex = 0;
        }

        // Update page with new character info
        UpdatePage(profiles[currentCharacterIndex]);
    }

    public void DisplayPreviousCharacter()
    {
        // Stop function when there are no profiles
        if (profiles.Count <= 0) { return; }

        // Reduce the current index
        currentCharacterIndex--;
        // Check if the new index is less than 0 and if it is set the index to the last index in profile list
        // This makes the palyer go to the last page when they try to go past the first page
        if (currentCharacterIndex < 0)
        {
            currentCharacterIndex = profiles.Count - 1;
        }
        
        // Update page with new character info
        UpdatePage(profiles[currentCharacterIndex]);
    }
}
