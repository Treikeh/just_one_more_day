using TMPro;
using UnityEngine;

// !TODO: Find a better way of getting active and finished quests from the QuestManager. Don't want to use public static variables

public class JournalUi : MonoBehaviour
{
    [SerializeField] private TMP_Text activeQuests;
    [SerializeField] private TMP_Text finishedQuests;


    private void OnEnable()
    {
        // Display active quests
        activeQuests.text = "Active Quessts:\n";
        foreach (string key in QuestManager.activeQuests.Keys)
        {
            activeQuests.text += $"{QuestManager.activeQuests[key].info.description}: {QuestManager.activeQuests[key].questProgress}/{QuestManager.activeQuests[key].info.questSteps}\n";
        }

        // Display finished quests
        finishedQuests.text = "Finished Quests:\n";
        foreach (string key in QuestManager.finishedQuests.Keys)
        {
            finishedQuests.text += $"{QuestManager.finishedQuests[key].info.description}\n";
        }
    }
}
