using UnityEngine;

public class Credits : MonoBehaviour
{

    private void Start()
    {
        QuestManager.Instance.hasJournal = false;
    }


    public void OnMainMenuButtonPressed()
    {
        LevelManager.Instance.StartLoadingLevel("MainMenu");
    }
}
