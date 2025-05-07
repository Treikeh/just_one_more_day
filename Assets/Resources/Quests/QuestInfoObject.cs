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
        QuestManager.Instance.StartQuest(this.name);
    }

    public void AdvanceQuest()
    {
        QuestManager.Instance.AdvanceQuest(this.name);
    }

    public void FinishQuest()
    {
        QuestManager.Instance.FinishQuest(this.name);
    }
}
