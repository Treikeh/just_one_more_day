using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


// TODO: Find a way to not connect the inputManager to this script


public class LevelManager : MonoBehaviour
{
    // Create singleton instance
    public static LevelManager Instance {get; private set;}
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    [SerializeField] private GameObject loadingScreen;

    public void StartLoadingScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        StartCoroutine(ProgressLoadingScene(scene));
    }


    private IEnumerator ProgressLoadingScene(AsyncOperation scene)
    {
        // Diable player input when loading scene
        InputManager.Instance.ToggleActionMap(InputManager.Instance.inputActions.Ui);
        // Show loading screen and stop scene from spawning until lodaing screen is fully visible
        scene.allowSceneActivation = false;
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(.5f);

        // Allow scene to spawn when ready
        scene.allowSceneActivation = true;
        // Check if the scene has finished loading
        while (!scene.isDone) { yield return null; }

        // Hide loading screen when scene is ready
        yield return new WaitForSeconds(.5f);
        loadingScreen.SetActive(false);
        // Enalbe player inputs when scene has finished loading
        InputManager.Instance.ToggleActionMap(InputManager.Instance.inputActions.Player);
    }
}
