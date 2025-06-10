using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button loadGameButton;


    private void Start()
    {
        if (!SaveManager.SaveGameExist())
        {
            loadGameButton.interactable = false;
        }
    }


    public void OnNewGameButtonPressed()
    {
        SaveManager.DeleteSaveGame();
        LevelManager.Instance.StartLoadingLevel("Mika's Room", default, false);
    }

    public void OnLoadGameButtonPressed()
    {
        SaveManager.LoadGame();
    }

    public void OnControlsMenuButtonPressed()
    {
        LevelManager.Instance.StartLoadingLevel("ControlsMenu", default, true);
    }

    public void OnCreditsButtonPressed()
    {
        LevelManager.Instance.StartLoadingLevel("Credits", default, true);
    }
}
