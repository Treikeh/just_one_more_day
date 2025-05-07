using UnityEngine;
using UnityEngine.Events;

public class QuestProgressChecker : MonoBehaviour, IInteractable
{
    [SerializeField] private QuestInfoObject questToCheck;
    public UnityEvent questNotStarted;
    public UnityEvent questInProgress;
    public UnityEvent questCanFinish;
    public UnityEvent questFinished;
    private QuestState currentQuestState = QuestState.NOT_STARTED;


    private void OnEnable() { QuestManager.Instance.onQuestStateChanged += QuestStateChanged; }
    private void OnDisable() { QuestManager.Instance.onQuestStateChanged -= QuestStateChanged; }
    private void QuestStateChanged(Quest quest)
    {
        if (quest.info.name.Equals(questToCheck.name))
        {
            currentQuestState = quest.state;
            Debug.Log($"Quest Check {currentQuestState}");
        }
    }


    private void Start()
    {
        currentQuestState = QuestManager.Instance.GetQuestState(questToCheck.name);
    }


public void Interact()
    {
        CheckQuestProgress();
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
            case QuestState.CAN_FINISH:
                questCanFinish.Invoke();
                break;
            case QuestState.FINISHED:
                questFinished.Invoke();
                break;
        }
    }
}
