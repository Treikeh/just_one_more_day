using UnityEngine;
using UnityEngine.Events;

public class QuestProgressChecker : MonoBehaviour
{
    [SerializeField] private QuestInfoObject questToCheck;
    public UnityEvent questNotStarted;
    public UnityEvent questInProgress;
    public UnityEvent questFinished;


    public void CheckQuestProgress()
    {
        // Get quest info from quest manager
        QuestState progress = QuestState.FINISHED;
        switch (progress)
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
}
