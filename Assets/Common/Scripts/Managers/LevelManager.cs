using System;
using System.Collections;
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

        OnLevelLoaded?.Invoke();
        // Hide loading screen when scene is ready
        // Start hide loading screen animation
        loadingScreenAnimator.Play("LoadingScreen_Hide");
        yield return new WaitForSeconds(.25f);
        // Disable loading screen when the animation has finished
        loadingScreen.SetActive(false);
    }
}
