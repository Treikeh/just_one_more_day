using UnityEngine;

public class ControlsMenu : MonoBehaviour
{
    public void OnMainMenuButtonPressed()
    {
        LevelManager.Instance.StartLoadingLevel("MainMenu", default, true);
    }
}
