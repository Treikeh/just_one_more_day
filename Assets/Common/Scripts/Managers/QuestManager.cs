using UnityEngine;
//CREDITS: Shaped by Rain Studios - Youtube: https://www.youtube.com/watch?v=UyTJLDGcT64

public class QuestManager : MonoBehaviour
{
    // Create singleton instance
    public static QuestManager Instance {get; private set;}
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    // 
}
