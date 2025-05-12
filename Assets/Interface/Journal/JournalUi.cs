using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class JournalUi : MonoBehaviour
{
    [SerializeField] private TMP_Text activeQuestsText;
    [SerializeField] private TMP_Text finishedQuestsText;
    [SerializeField] private TMP_Text characterProfilesText;

    [SerializeField] private GameObject questsPanel;
    [SerializeField] private GameObject charactersPanel;



    private void OnEnable()
    {
        // Display active quests
        Dictionary<string, Quest> activeQuests = QuestManager.Instance.ActiveQuests;
        activeQuestsText.text = "Active Quessts:\n";
        foreach (string key in activeQuests.Keys)
        {
            activeQuestsText.text += $"{activeQuests[key].info.description}: {activeQuests[key].questProgress}/{activeQuests[key].info.questSteps}\n";
        }

        // Display finished quests
        Dictionary<string, Quest> finishedQuests = QuestManager.Instance.FinishedQuests;
        finishedQuestsText.text = "Finished Quests:\n";
        foreach (string key in finishedQuests.Keys)
        {
            finishedQuestsText.text += $"{finishedQuests[key].info.description}\n";
        }

        // Display character profiles
        characterProfilesText.text = "People:\n";
        foreach (CharacterProfileSO profile in UiManager.Instance.GetCharacterProfiles())
        {
            characterProfilesText.text += $"{profile.characterName} - {profile.characterDescription}\n";
        }
    }

    public void ShowPanel(int index)
    {
        CloseAllPanels();
        switch (index)
        {
            case 0:
                questsPanel.SetActive(true);
                break;
            case 1:
                charactersPanel.SetActive(true);
                break;
        }
    }

    public void CloseAllPanels()
    {
        questsPanel.SetActive(false);
        charactersPanel.SetActive(false);
    }
}
