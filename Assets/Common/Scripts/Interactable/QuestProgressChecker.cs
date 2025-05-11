using UnityEngine;
using UnityEngine.Events;

public class QuestProgressChecker : MonoBehaviour, IInteractable
{
    [SerializeField] private QuestInfoSO questToCheck;

    public UnityEvent questNotStarted;
    public UnityEvent questInProgress;
    public UnityEvent questCanFinish;
    public UnityEvent questFinished;


public void Interact()
    {
        CheckQuestProgress();
    }

    public void CheckQuestProgress()
    {
        QuestState state = QuestManager.Instance.GetQuestState(questToCheck.name);
        switch (state)
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
