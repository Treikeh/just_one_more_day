using System;

public class LevelEvents
{
    public event Action<string> onLoadLevel;
    public void LoadLevel(string level)
    {
        if (onLoadLevel != null)
        {
            onLoadLevel(level);
        }
    }
}
