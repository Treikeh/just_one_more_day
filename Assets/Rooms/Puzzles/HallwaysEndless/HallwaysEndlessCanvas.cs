using UnityEngine;


public class HallwaysEndlessCanvas : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitSound;


    private void OnEnable() { EndlessObstacle.ObstacleHit += OnObstacleHit; }
    private void OnDisable() { EndlessObstacle.ObstacleHit -= OnObstacleHit; }


    public void ShowLives(bool value)
    {
        animator.SetBool("showLives", value);
    }


    private void OnObstacleHit(int livesRemaining)
    {
        animator.SetInteger("lives", livesRemaining);
        audioSource.PlayOneShot(hitSound);
    }
}
