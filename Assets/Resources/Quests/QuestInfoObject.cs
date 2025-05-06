using UnityEngine;


[CreateAssetMenu(fileName = "NewQuestInfoObject", menuName = "Data/QuestInfo", order = 1)]
public class QuestInfoObject : ScriptableObject
{
    [Header("General")]
    public string description;
    public bool finishAutomatically = false;

    [Header("Requirements")]
    public QuestInfoObject[] questPrerequisites;
    public int questSteps = 1;

    public void StartQuest()
    {
        GameEventManager.Instance.questEvents.QuestStarted(this.name);
    }

    public void AdvanceQuest()
    {
        GameEventManager.Instance.questEvents.QuestAdvanced(this.name);
    }

    public void FinishQuest()
    {
        GameEventManager.Instance.questEvents.QuestFinished(this.name);
    }
}
