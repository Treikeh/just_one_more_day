using UnityEngine;
using UnityEngine.Events;

// See I can make a simple timer

public class SimpleTimer : MonoBehaviour
{
    [SerializeField] private float waitTime = 5.0f;
    [SerializeField] private bool startOnStart = true;
    public UnityEvent timeout;


    void Start()
    {
        if (startOnStart)
        {
            Invoke(nameof(TimeOut), waitTime);
        }
    }

    private void TimeOut()
    {
        timeout?.Invoke();
    }


    public void StartTimer()
    {
            Invoke(nameof(TimeOut), waitTime);
    }
}
