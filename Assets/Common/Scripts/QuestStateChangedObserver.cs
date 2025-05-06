using UnityEngine;
using UnityEngine.Events;

// This component sends events when the QuestInfo it observes chagnes state


public class QuestStateChangedObserver : MonoBehaviour
{
    [SerializeField] private QuestInfoObject questInfoObject;

    public UnityEvent notStartedResponse;
    public UnityEvent inProgressResponse;
    public UnityEvent canFinishResponse;
    public UnityEvent finishedResponse;


    private void OnEnable() { GameEventManager.Instance.questEvents.onQuestStateChanged += QuestStateChanged; }
    private void OnDisable() { GameEventManager.Instance.questEvents.onQuestStateChanged -= QuestStateChanged; }

    private void Start()
    {
        // Check the state of the quest when the game starts
        GameEventManager.Instance.questEvents.QuestRefreshed(questInfoObject.name);
    }

    private void QuestStateChanged(Quest quest)
    {
        if (quest.info.name.Equals(questInfoObject.name))
        {
            switch (quest.state)
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
}
