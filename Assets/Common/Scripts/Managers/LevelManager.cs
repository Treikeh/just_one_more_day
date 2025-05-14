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
    }


// *SAVE SYSTEM
    // This could be stored in any script that is globaly avalible
    private Dictionary<string, object> saveDict = new();
    public void SetSaveData(string key, object data)
    {
        // Set data
        if (saveDict.ContainsKey(key))
            { saveDict[key] = data; }
        // Create data
        else
            { saveDict.Add(key, data); }
    }

    public object GetSaveData(string key)
    {
        object data = new();
        if (saveDict.ContainsKey(key))
        {
            data = saveDict[key];
        }
        return data;
    }
}
