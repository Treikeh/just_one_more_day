using UnityEngine;
//CREDITS: Shaped by Rain Studios - Github: https://github.com/shapedbyrainstudios/quest-system

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager Instance { get; private set; }


    public LevelEvents levelEvents;
    public UiEvents uiEvents;
    public QuestEvents questEvents;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("GameEventManager allready exists");
            Destroy(this);
        }
        else
        {
            Instance = this;

            levelEvents = new LevelEvents();
            uiEvents = new UiEvents();
            questEvents = new QuestEvents();
        }
    }
}
