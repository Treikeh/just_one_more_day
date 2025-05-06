using UnityEngine;
using UnityEngine.Events;

// Sends an event when the player hits a trigger.
// If you want to disable the trigger after the player walks into it use the event to disable the trigger collider
public class BasicTrigger : MonoBehaviour
{
    public UnityEvent playerEnteredTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerEnteredTrigger.Invoke();
        }
    }
}
