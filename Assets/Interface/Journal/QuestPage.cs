using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestPage : MonoBehaviour
{
    [SerializeField] private TMP_Text activeQuestsText;
    [SerializeField] private TMP_Text finishedQuestsText;


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
    }
}
