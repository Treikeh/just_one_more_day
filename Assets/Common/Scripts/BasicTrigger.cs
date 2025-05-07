using UnityEngine;
using UnityEngine.Events;

// Sends an event when the player hits a trigger.
// If you want to disable the trigger after the player walks into it use the event to disable the trigger collider
public class BasicTrigger : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";
    public UnityEvent targetEnteredTrigger;
    public UnityEvent targetExitedTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == targetTag)
        {
            targetEnteredTrigger.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == targetTag)
        {
            targetExitedTrigger.Invoke();
        }
    }
}
