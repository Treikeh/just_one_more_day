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
        LevelManager.Instance.StartLoadingLevel("Mika's Room");
    }

    public void OnLoadGameButtonPressed()
    {
        SaveManager.LoadGame();
    }
}
