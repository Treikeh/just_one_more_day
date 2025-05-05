using UnityEngine;
using UnityEngine.Events;

public class QuestProgressChecker : MonoBehaviour, IInteractable
{
    [SerializeField] private QuestInfoObject questToCheck;
    public UnityEvent questNotStarted;
    public UnityEvent questInProgress;
    public UnityEvent questFinished;
    private QuestState currentQuestState = QuestState.NOT_STARTED;


    private void OnEnable() { GameEventManager.Instance.questEvents.onQuestStateChanged += QuestStateChanged; }
    private void OnDisable() { GameEventManager.Instance.questEvents.onQuestStateChanged -= QuestStateChanged; }

    private void QuestStateChanged(Quest quest)
    {
        if (quest.info.name.Equals(questToCheck.name))
        {
            currentQuestState = quest.state;
            Debug.Log($"Quest Check {currentQuestState}");
        }
    }


    public void CheckQuestProgress()
    {
        switch (currentQuestState)
        {
            case QuestState.NOT_STARTED:
                questNotStarted.Invoke();
                break;
            case QuestState.IN_PROGRESS:
                questInProgress.Invoke();
                break;
            case QuestState.FINISHED:
                questFinished.Invoke();
                break;
        }
    }


    public void Interact()
    {
        CheckQuestProgress();
    }
}
