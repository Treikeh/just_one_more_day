using UnityEngine;
//CREDITS: Shaped by Rain Studios - Github: https://github.com/shapedbyrainstudios/quest-system

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager Instance { get; private set; }


    public InputEvents inputEvents;
    public LevelEvents levelEvents;
    public DialogueEvents dialogueEvents;
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

            inputEvents = new InputEvents();
            levelEvents = new LevelEvents();
            dialogueEvents = new DialogueEvents();
            questEvents = new QuestEvents();
        }
    }
}
