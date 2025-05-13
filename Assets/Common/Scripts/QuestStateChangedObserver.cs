using UnityEngine;
using UnityEngine.Events;

// This component sends events when the QuestInfo it observes chagnes state

public class QuestStateChangedObserver : MonoBehaviour
{
    [SerializeField] private QuestInfoSO questToCheck;

    public UnityEvent notStartedResponse;
    public UnityEvent inProgressResponse;
    public UnityEvent canFinishResponse;
    public UnityEvent finishedResponse;


    private void OnEnable() { QuestManager.Instance.OnQuestStateChanged += QuestStateChanged; }
    private void OnDisable() { QuestManager.Instance.OnQuestStateChanged -= QuestStateChanged; }


    private void Start()
    {
        // Check quest when the game starts
        CheckQuestState(QuestManager.Instance.GetQuestState(questToCheck.name));
    }


    private void QuestStateChanged(Quest quest)
    {
        if (quest.info.name.Equals(questToCheck.name))
        {
            CheckQuestState(quest.state);
        }
    }

    private void CheckQuestState(QuestState state)
    {
        switch (state)
        {
            case QuestState.NOT_STARTED:
                notStartedResponse.Invoke();
                break;
            case QuestState.IN_PROGRESS:
                inProgressResponse.Invoke();
                break;
            case QuestState.CAN_FINISH:
                canFinishResponse.Invoke();
                break;
            case QuestState.FINISHED:
                finishedResponse.Invoke();
                break;
        }
    }
}
