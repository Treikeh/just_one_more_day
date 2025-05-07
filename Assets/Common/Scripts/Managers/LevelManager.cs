using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public static event Action OnLevelLoaded;

    public static LevelManager Instance { get; private set; }

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


    public Dictionary<string, int> saveDict = new();

    private void SaveAllObjects()
    {
        var iSaveList = FindObjectsOfType<MonoBehaviour>().OfType<ISave>();
        foreach (ISave s in iSaveList)
        {
            s.Save();
        }
    }


    public void StartLoadingLevel(string sceneName)
    {
        SaveAllObjects();
        var scene = SceneManager.LoadSceneAsync(sceneName);
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
}
