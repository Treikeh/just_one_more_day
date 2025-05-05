using System;

public class QuestEvents
{
    public event Action<string> onQuestStarted;
    public void QuestStarted(string questId)
    {
        if (onQuestStarted != null)
        {
            onQuestStarted(questId);
        }
    }

    public event Action<string> onQuestAdvanced;
    public void QuestAdvanced(string questId)
    {
        if (onQuestAdvanced != null)
        {
            onQuestAdvanced(questId);
        }
    }

    public event Action<string> onQuestFinished;
    public void QuestFinished(string questId)
    {
        if (onQuestFinished != null)
        {
            onQuestFinished(questId);
        }
    }

    public event Action<Quest> onQuestStateChanged;
    public void QuestStateChanged(Quest quest)
    {
        if (onQuestStateChanged != null)
        {
            onQuestStateChanged(quest);
        }
    }
}
