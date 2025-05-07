using UnityEngine;

public class LevelLoaderTrigger : MonoBehaviour
{
    [SerializeField] string levelToLoad = "";


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartLoadingLevel();
        }
    }

    public void StartLoadingLevel()
    {
        LevelManager.Instance.StartLoadingLevel(levelToLoad);
    }
}
