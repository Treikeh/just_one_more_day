using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ListInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private List<UnityEvent> indexResponses;
    public UnityEvent defaultResponse;
    private int timesInteracted = 0;


    public void Interact()
    {
        if (timesInteracted <= indexResponses.Count - 1)
        {
            indexResponses[timesInteracted]?.Invoke();
            timesInteracted++;
        }
        else
        {
            defaultResponse?.Invoke();
        }
    }
}