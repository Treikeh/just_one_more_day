using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndlessObstacle : MonoBehaviour
{
    private static int lives = 3;
    private float flashDelay = 0.15f;


    private void Start()
    {
        lives = 3;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Questionable code time
        if (other.tag == "Player")
        {
            lives--;
            if (lives <= 0)
            {
                LevelManager.Instance.StartLoadingLevel(SceneManager.GetActiveScene().name);
            }
            SpriteRenderer playerSprite = other.gameObject.GetComponentInChildren<SpriteRenderer>();
            StopAllCoroutines();
            StartCoroutine(SpriteFlashing(playerSprite));
        }
    }


    private IEnumerator SpriteFlashing(SpriteRenderer sprite)
    {
        sprite.enabled = false;
        yield return new WaitForSeconds(flashDelay);
        sprite.enabled = true;
        yield return new WaitForSeconds(flashDelay);
        sprite.enabled = false;
        yield return new WaitForSeconds(flashDelay);
        sprite.enabled = true;
        yield return new WaitForSeconds(flashDelay);
        sprite.enabled = false;
        yield return new WaitForSeconds(flashDelay);
        sprite.enabled = true;
    }
}
