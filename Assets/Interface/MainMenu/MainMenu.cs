using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnPlayButtonPressed()
    {
        if (SaveManager.SaveGameExist())
        {
            SaveManager.LoadGame();
        }
        else
        {
            LevelManager.Instance.StartLoadingLevel("Mika's Room");
        }
    }

    public void OnQuitGamePressed()
    {
        Application.Quit();
    }

    public void OnDeleteSaveGameButtonPressed()
    {
        SaveManager.DeleteSaveGame();
    }
}
