using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewGameEvent", menuName = "Data/GameEvent")]
public class GameEventSO : ScriptableObject
{
    // All objects that are observing this event
    private readonly List<GameEventObjectObserver> observers = new();

    // Add new object to list of observers
    public void AddObserver(GameEventObjectObserver observer)
    {
        observers.Add(observer);
    }

    // Remove object from list of observers
    public void RemoveObserver(GameEventObjectObserver observer)
    {
        observers.Remove(observer);
    }

    // Call this method when you want this GameEvent to be triggered
    public void TriggerEvent()
    {
        // Debug log to see which and when a event is triggered.
        // Debug.Log("GameEvent " + name + " Triggered");
        // When this event is triggered, go through every observer it has and activate their response
        foreach (GameEventObjectObserver observer in observers)
        {
            observer.RespondToEvent(name);
        }
    }
}
