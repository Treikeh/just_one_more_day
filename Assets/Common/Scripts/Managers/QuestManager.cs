using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//CREDITS: Shaped by Rain Studios - Github: https://github.com/shapedbyrainstudios/quest-system - Youtube: https://www.youtube.com/watch?v=UyTJLDGcT64
// It's not an excat copy their code but a more simplified version.

public class QuestManager : MonoBehaviour
{
    // Create singleton instance
    public static QuestManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        questMap = CreateQuestMap();
    }



    private Dictionary<string, Quest> questMap;
    public Dictionary<string, Quest> ActiveQuests { get; private set; } = new();
    public Dictionary<string, Quest> FinishedQuests { get; private set; } = new();

    public event Action<Quest> onQuestStateChanged;



    // Get all quests in the Assets/Resources/Quests folder
    private Dictionary<string, Quest> CreateQuestMap()
    {
        QuestInfoObject[] allQuests = Resources.LoadAll<QuestInfoObject>("Quests");

        Dictionary<string, Quest> idToQuestMap = new();
        foreach (QuestInfoObject questInfoObject in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfoObject.name))
            {
                Debug.LogWarning("Quest is allready in the map");
            }
            idToQuestMap.Add(questInfoObject.name, LoadQuest(questInfoObject));
        }
        return idToQuestMap;
    }

    // Can use this function to load quest data from save game, when saving is added. Use try and catch
    private Quest LoadQuest(QuestInfoObject questInfo)
    {
        Quest quest = new(questInfo);
        return quest;
    }

    public Quest GetQuestById(string questId)
    {
        Quest quest = questMap[questId];
        if (quest == null)
        {
            Debug.LogError($"{questId} not found in the quest map");
        }
        return quest;
    }

    private void ChangeQuestState(Quest quest, QuestState questState)
    {
        quest.state = questState;
        onQuestStateChanged?.Invoke(quest);
    }


    public void StartQuest(string questId)
    {
        Quest quest = GetQuestById(questId);
        // TODO: Make sure all prerequisite quest are completed
        if (quest.state != QuestState.NOT_STARTED)
        {
            Debug.Log("Quest has allready started or is finished");
            return;
        }
        Debug.Log($"{questId} Started");
        ActiveQuests.Add(questId, quest);
        ChangeQuestState(quest, QuestState.IN_PROGRESS);
    }

    public void AdvanceQuest(string questId)
    {
        Quest quest = GetQuestById(questId);

        // Check if quest has started. Don't advance quest if it hasn't started yet
        if (quest.state != QuestState.IN_PROGRESS)
        {
            Debug.Log("Quest has not started yet or has allready been finished");
            return;
        }
        // Add progress to quest
        quest.questProgress++;
        Debug.Log($"{questId} progress is {quest.questProgress}/{quest.info.questSteps}");

        // Finish the quest when all steps are completed
        if (quest.questProgress >= quest.info.questSteps)
        {
            ChangeQuestState(quest, QuestState.CAN_FINISH);
            Debug.Log($"{questId} can be finished");
            if (quest.info.finishAutomatically)
            {
                FinishQuest(questId);
            }
        }
    }

    public void FinishQuest(string questId)
    {
        Debug.Log("Hello");
        Quest quest = GetQuestById(questId);

        if (quest.state != QuestState.CAN_FINISH)
        {
            Debug.Log("Quest cannot be finished yet");
            return;
        }

        Debug.Log($"{questId} Finished");
        ActiveQuests.Remove(questId);
        FinishedQuests.Add(questId, quest);
        ChangeQuestState(quest, QuestState.FINISHED);
    }

    public QuestState GetQuestState(string questId)
    {
        Quest quest = GetQuestById(questId);
        return quest.state;
    }
}


public enum QuestState
{
    NOT_STARTED,
    IN_PROGRESS,
    CAN_FINISH,
    FINISHED,
}


public class Quest
{
    public QuestInfoObject info;
    public QuestState state;
    public int questProgress;

    public Quest(QuestInfoObject questInfo, QuestState questState, int progress)
    {
        this.info = questInfo;
        this.state = questState;
        this.questProgress = progress;
    }

    public Quest(QuestInfoObject questInfo)
    {
        this.info = questInfo;
        this.state = QuestState.NOT_STARTED;
        this.questProgress = 0;
    }
}