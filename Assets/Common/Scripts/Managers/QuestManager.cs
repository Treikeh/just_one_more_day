using System;
using System.Collections.Generic;
using UnityEngine;
//CREDITS: Shaped by Rain Studios - Youtube: https://www.youtube.com/watch?v=UyTJLDGcT64

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;

    private void Awake()
    {
        CreateQuestMap();
        // questMap = CreateQuestMap();
    }

    private void OnEnable()
    {
        GameEventManager.Instance.questEvents.onQuestStarted += QuestStarted;
        GameEventManager.Instance.questEvents.onQuestAdvanced += QuestAdvanced;
        GameEventManager.Instance.questEvents.onQuestStarted += QuestFinished;
    }

    private void OnDisable()
    {
        GameEventManager.Instance.questEvents.onQuestStarted -= QuestStarted;
        GameEventManager.Instance.questEvents.onQuestAdvanced -= QuestAdvanced;
        GameEventManager.Instance.questEvents.onQuestStarted -= QuestFinished;
    }

    private void CreateQuestMap()
    {
        QuestInfoObject[] allQuests = Resources.LoadAll<QuestInfoObject>("../Data/Quests");
        Debug.Log(allQuests.Length);
    }


    private void QuestFinished(string questId)
    {
        //
    }

    private void QuestAdvanced(string questId)
    {
        //
    }

    private void QuestStarted(string questId)
    {
        //
    }


    private QuestState GetQuestState()
    {
        return QuestState.IN_PROGRESS;
    }
}


public enum QuestState
{
    NOT_STARTED,
    IN_PROGRESS,
    FINISHED,
}


public class Quest
{
    public QuestInfoObject info;
    public QuestState state;
    public int questProgress;
}