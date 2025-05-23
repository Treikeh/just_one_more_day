using UnityEngine;
using UnityEngine.Events;

public class HiddenObjectPuzzle : MonoBehaviour
{
    public UnityEvent allObjectsRemovedResponse;

    private int objectsToRemove;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log($"Objects to remove {objectsToRemove}");
            objectsToRemove++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            objectsToRemove--;
            Debug.Log($"Objects to remove {objectsToRemove}");
            if (objectsToRemove <= 0)
            {
                Debug.Log("All objects removed");
                allObjectsRemovedResponse?.Invoke();
            }
        }
    }
}
