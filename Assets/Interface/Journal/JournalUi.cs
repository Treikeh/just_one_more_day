using TMPro;
using UnityEngine;

public class JournalUi : MonoBehaviour
{
    [SerializeField] private GameObject journalPanel;
    [SerializeField] private TMP_Text activeQuests;
    [SerializeField] private TMP_Text finishedQuests;

    private void OnEnable()
    {
        GameEventManager.Instance.inputEvents.onJournalPressed += JounralPressed;
        GameEventManager.Instance.inputEvents.onCancelPressed += CancelPressed;
    }

    private void OnDisable()
    {
        GameEventManager.Instance.inputEvents.onJournalPressed -= JounralPressed;
        GameEventManager.Instance.inputEvents.onCancelPressed -= CancelPressed;
    }


    // Hide Journal
    private void CancelPressed()
    {
        if (journalPanel.activeInHierarchy)
        {
            journalPanel.SetActive(false);
            GameEventManager.Instance.inputEvents.ActionMapChanged("Player");
        }
    }

    // Show Journal
    private void JounralPressed()
    {
        if (!journalPanel.activeInHierarchy)
        {
            journalPanel.SetActive(true);
            GameEventManager.Instance.inputEvents.ActionMapChanged("Ui");

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
                finishedQuests.text += $"{QuestManager.finishedQuests[key].info.description}";
            }
        }
    }

    // UI BUTTON EVENTS
    public void OnResumePressed()
    {
        // Hide jounral
        CancelPressed();
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }
}
