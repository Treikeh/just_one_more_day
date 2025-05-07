using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] string sceneToLoad = "";


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartLoadingLevel();
        }
    }

    public void StartLoadingLevel()
    {
        LevelManager.Instance.StartLoadingLevel(sceneToLoad);
    }
}
