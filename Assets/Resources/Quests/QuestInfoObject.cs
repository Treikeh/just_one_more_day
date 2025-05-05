using UnityEngine;


[CreateAssetMenu(fileName = "NewQuestInfoObject", menuName = "Data/QuestInfo", order = 1)]
public class QuestInfoObject : ScriptableObject
{
    [field: SerializeField] private string id;

    [Header("General")]
    public string displayName;

    [Header("Requirements")]
    public QuestInfoObject[] questPrerequisites;
}
