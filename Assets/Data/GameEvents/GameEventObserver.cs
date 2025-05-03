using UnityEngine;
using UnityEngine.Events;

public class GameEventObserver : MonoBehaviour
{
    // Every GameEvent and its response that this component is observing
    public GameEvent gameEvent;

    // You can connect this to many different objects in the same way you would with Ui and they will all activate when the observed Event is triggered
    public UnityEvent response;


    private void OnEnable()
    {
        gameEvent.AddObserver(this);
    }

    private void OnDisable()
    {
        gameEvent.RemoveObserver(this);
    }

    public void RespondToEvent(string eventName)
    {
        if (gameEvent.name == eventName)
        {
            response.Invoke();
        }
    }
}

