using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


// TODO: Find a way to not connect the inputManager to this script


public class LevelManager : MonoBehaviour
{
    // Create singleton instance
    public static LevelManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }



    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Animator loadingScreenAnimator;
    public static event Action onLevelLoaded;


    private void Start()
    {
        // Trigger level loded event when the game starts.
        // This is to make sure that objects that depend on this event can setup correctly.
        onLevelLoaded?.Invoke();
    }


    public void StartLoadingLevel(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        StartCoroutine(ProgressLoadingScene(scene));
    }

    private IEnumerator ProgressLoadingScene(AsyncOperation scene)
    {
        // Diable player input when loading scene
        InputManager.Instance.ChangeActionMap("Ui");
        // Show loading screen and stop scene from spawning until lodaing screen is fully visible
        scene.allowSceneActivation = false;
        loadingScreen.SetActive(true);
        // Start show loading screen animation
        loadingScreenAnimator.Play("LoadingScreen_Show");
        yield return new WaitForSeconds(.25f);

        // Allow scene to spawn when ready
        scene.allowSceneActivation = true;
        // Check if the scene has finished loading
        while (!scene.isDone) { yield return null; }

        // Hide loading screen when scene is ready
        // Start hide loading screen animation
        loadingScreenAnimator.Play("LoadingScreen_Hide");
        onLevelLoaded?.Invoke();
        yield return new WaitForSeconds(.25f);
        loadingScreen.SetActive(false);
        // Enalbe player inputs when scene has finished loading
        // GameEventManager.Instance.inputEvents.ActionMapChanged("Player");
    }
}
