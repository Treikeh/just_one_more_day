using UnityEngine;

public class LevelLoaderTrigger : MonoBehaviour
{
    [SerializeField] private string levelToLoad = "";
    [SerializeField] private Transform spawnTransform;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartLoadingLevel();
        }
    }

    public void StartLoadingLevel()
    {
        Vector2 spawnPositon = spawnTransform != null ? spawnTransform.position: Vector2.zero;
        LevelManager.Instance.StartLoadingLevel(levelToLoad, spawnPositon);
    }
}
