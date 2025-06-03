using System.Collections.Generic;
using TMPro;
using UnityEngine;

// TODO: Change how quests are displayed. Don't use a single text component for all active and finished quests

public class QuestPage : MonoBehaviour
{
    [SerializeField] private TMP_Text activeQuestsText;
    [SerializeField] private TMP_Text finishedQuestsText;


    private void OnEnable()
    {
        // Display active quests
        // Get all active quests from the QuestManager
        Dictionary<string, Quest> activeQuests = QuestManager.Instance.ActiveQuests;
        activeQuestsText.text = "";
        // Go through every active quest and add the quest description and progress to activeQuestsText
        foreach (string key in activeQuests.Keys)
        {
            activeQuestsText.text += $"{activeQuests[key].info.description}: {activeQuests[key].questProgress}/{activeQuests[key].info.questSteps}\n";
        }

        // Display finished quests
        // Get all finished quests from the QuestManager
        Dictionary<string, Quest> finishedQuests = QuestManager.Instance.FinishedQuests;
        // Reset finishedQuestsText.
        finishedQuestsText.text = "";
        // Go through every finished quest and add the quest description to finishedQuestsText
        foreach (string key in finishedQuests.Keys)
        {
            finishedQuestsText.text += $"{finishedQuests[key].info.description}\n";
        }
    }
}
