using System;

public class LevelEvents
{
    public event Action<string> onStartLoadingLevel;
    public void StartLoadingLevel(string level)
    {
        if (onStartLoadingLevel != null)
        {
            onStartLoadingLevel(level);
        }
    }

    public event Action onLevelLoaded;
    public void LevelLoaded()
    {
        if (onLevelLoaded != null)
        {
            onLevelLoaded();
        }
    }
}
