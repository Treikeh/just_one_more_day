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


    private void OnEnable() { QuestManager.Instance.onQuestStateChanged += QuestStateChanged; }
    private void OnDisable() { QuestManager.Instance.onQuestStateChanged -= QuestStateChanged; }


    private void Start()
    {
        QuestStateChanged(QuestManager.Instance.GetQuestById(questInfoObject.name));
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
