using UnityEngine;

public class Credits : MonoBehaviour
{
    public void OnMainMenuButtonPressed()
    {
        LevelManager.Instance.StartLoadingLevel("MainMenu");
    }
}
