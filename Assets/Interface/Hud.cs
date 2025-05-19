using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Hud : MonoBehaviour
{
    private string saveFilePath;


    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
        saveFilePath = Application.persistentDataPath + "/SaveData.json";
    }


    public void SaveGame()
    {
        GameSaveData saveData = new()
        {
            // Save current level
            levelName = SceneManager.GetActiveScene().name,
            // Save story progress
            storyProgress = QuestManager.Instance.storyProgress,
            // Save character profiles
            characterProfiles = UiManager.Instance.characterProfiles,

            questMapData = new(),
            levelSaveData = new(),
        };

        // Save QuestManagers questMap
        foreach (KeyValuePair<string, Quest> item in QuestManager.Instance.questMap)
        {
            QuestMapDictPair save = new()
            {
                key = item.Key,
                quest = item.Value,
            };
            saveData.questMapData.Add(save);
        }

        // Save LevelManagers levelSaveDict
        foreach (KeyValuePair<string, LevelSaveData> item in LevelManager.Instance.levelSaveDict)
        {
            LevelSaveDictPair save = new()
            {
                key = item.Key,
                data = item.Value
            };
            saveData.levelSaveData.Add(save);
        }

        // Turn GameSaveData into json
        File.WriteAllText(saveFilePath, JsonUtility.ToJson(saveData));
    }


    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            // Load GameSaveData from file
            GameSaveData loadData = JsonUtility.FromJson<GameSaveData>(File.ReadAllText(saveFilePath));

            // Set story progress
            QuestManager.Instance.storyProgress = loadData.storyProgress;

            // Load character profiles
            UiManager.Instance.characterProfiles = loadData.characterProfiles;

            // Clear active and finished quests
            QuestManager.Instance.ActiveQuests.Clear();
            QuestManager.Instance.FinishedQuests.Clear();

            // Load QuestManagers questMap
            foreach (QuestMapDictPair pair in loadData.questMapData)
            {
                if (QuestManager.Instance.questMap.ContainsKey(pair.key))
                {
                    // Set questMap value
                    QuestManager.Instance.questMap[pair.key] = pair.quest;

                    // Set active and finished quests
                    if (pair.quest.state == QuestState.IN_PROGRESS || pair.quest.state == QuestState.CAN_FINISH)
                    {
                        QuestManager.Instance.ActiveQuests.Add(pair.key, pair.quest);
                    }
                    else if (pair.quest.state == QuestState.FINISHED)
                    {
                        QuestManager.Instance.FinishedQuests.Add(pair.key, pair.quest);
                    }
                }
            }

            // Load LevelManagers levelSaveDict
            foreach (LevelSaveDictPair pair in loadData.levelSaveData)
            {
                LevelManager.Instance.SetSaveData(pair.key, pair.data);
            }

            // Load level
            LevelManager.Instance.StartLoadingLevel(loadData.levelName);
        }
        else
        {
            Debug.Log("Save game doesn't exist");
        }
    }
}

public class GameSaveData
{
    public string levelName;
    public int storyProgress;
    public List<QuestMapDictPair> questMapData;
    public List<LevelSaveDictPair> levelSaveData;
    public List<CharacterProfileSO> characterProfiles;
}


[Serializable]
public class LevelSaveDictPair
{
    public string key;
    public LevelSaveData data;
}

[Serializable]
public class QuestMapDictPair
{
    public string key;
    public Quest quest;
}


