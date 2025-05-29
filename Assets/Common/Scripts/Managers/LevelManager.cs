using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public static event Action OnLevelLoaded;

    public static LevelManager Instance { get; private set; }
    // Having this as a public variable just so that the PlayerMovement script can read the value whe saving is stupid
    public Vector2 playerSpawnPosition { get; private set; }

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Animator loadingScreenAnimator;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }


    public void Reset()
    {
        levelSaveDict = new();
        playerSpawnPosition = Vector2.zero;
    }


private void Start()
    {
        // Trigger level loded event when the game starts.
        // This is to make sure that objects that depend on this event can setup correctly.
        OnLevelLoaded?.Invoke();
    }


    public void StartLoadingLevel(string sceneName, Vector2 spawnPosition = default)
    {
        playerSpawnPosition = spawnPosition;
        var scene = SceneManager.LoadSceneAsync(sceneName);
        // Stop all Coroutines to avoid the bug where the loading screen disappears when the player quickly goes into and out of a room 
        StopAllCoroutines();
        StartCoroutine(ProgressLoadingScene(scene));
    }


    private IEnumerator ProgressLoadingScene(AsyncOperation scene)
    {
        // Only allow new scene to spawn when the loading screen is shown
        scene.allowSceneActivation = false;
        // Diable player input when loading scene
        InputManager.Instance.ChangeActionMap("Ui");

        // Enable loading screen
        loadingScreen.SetActive(true);
        loadingScreenAnimator.Play("LoadingScreen_Show");
        yield return new WaitForSeconds(Utils.GetAnimationLength(loadingScreenAnimator, "LoadingScreen_Show"));

        // Allow scene to spawn when ready
        scene.allowSceneActivation = true;
        // Check if the scene has finished loading
        while (!scene.isDone) { yield return null; }
        // Trigger event that new level was loaded
        OnLevelLoaded?.Invoke();

        // Disable loading screen when scene has spawned
        loadingScreenAnimator.Play("LoadingScreen_Hide");
        yield return new WaitForSeconds(Utils.GetAnimationLength(loadingScreenAnimator, "LoadingScreen_Hide"));
        loadingScreen.SetActive(false);
        playerSpawnPosition = Vector2.zero;
    }


// *SAVE SYSTEM
    // This could be stored in any script that is globaly avalible
    public Dictionary<string, LevelSaveData> levelSaveDict = new();
    public void SetSaveData(string key, LevelSaveData data)
    {
        // Set data
        if (levelSaveDict.ContainsKey(key))
            { levelSaveDict[key] = data; }
        // Create data
        else
            { levelSaveDict.Add(key, data); }
    }

    public LevelSaveData GetSaveData(string key)
    {
        if (levelSaveDict.ContainsKey(key))
        {
            return levelSaveDict[key];
        }
        return null;
    }
}

[Serializable]
public class LevelSaveData
{
    public bool active;
    public string name;
    public Vector3 position;
}
