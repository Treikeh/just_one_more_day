using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SaveManager
{
    public static event Action GameLoaded;
    private static readonly string saveFileKey = "THIS_STRING_IS_WHAT_IS_USED_TO_IDENTIFY_THE_SAVE_GAME";


    public static void SaveGame()
    {
        GameSaveData saveData = new()
        {
            // Save current level
            levelName = SceneManager.GetActiveScene().name,
            hasJournal = QuestManager.Instance.hasJournal,
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
        //File.WriteAllText(saveFilePath, JsonUtility.ToJson(saveData));
        PlayerPrefs.SetString(saveFileKey, JsonUtility.ToJson(saveData));
        PlayerPrefs.Save();
    }


    public static void LoadGame()
    {
        //Debug.Log(saveFilePath);
        //Debug.Log(PlayerPrefs.GetString(saveFileKey));
        if (SaveGameExist())
        {
            // Load GameSaveData from file
            //GameSaveData loadData = JsonUtility.FromJson<GameSaveData>(File.ReadAllText(saveFilePath));
            GameSaveData loadData = JsonUtility.FromJson<GameSaveData>(PlayerPrefs.GetString(saveFileKey));

            QuestManager.Instance.hasJournal = loadData.hasJournal;

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
            LevelManager.Instance.StartLoadingLevel(loadData.levelName, default, false);
            GameLoaded?.Invoke();
        }
        else
        {
            Debug.Log("Save game doesn't exist");
        }
    }


    public static void DeleteSaveGame()
    {
        if (SaveGameExist())
        {
            PlayerPrefs.DeleteAll();
        }
        QuestManager.Instance.Reset();
        LevelManager.Instance.Reset();
    }


    public static bool SaveGameExist()
    {
        if (PlayerPrefs.HasKey(saveFileKey))
        {
            return true;
        }
        return false;
    }


    public class GameSaveData
    {
        public string levelName;
        public bool hasJournal;
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
}

/*
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
*/
