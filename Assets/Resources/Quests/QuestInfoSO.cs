using UnityEngine;


[CreateAssetMenu(fileName = "NewQuestInfo", menuName = "Data/Quest", order = 1)]
public class QuestInfoSO : ScriptableObject
{
    [Header("General")]
    public string description;
    public bool finishAutomatically = false;

    [Header("Requirements")]
    public QuestInfoSO[] questPrerequisites;
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
