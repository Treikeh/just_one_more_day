using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject defaultPage;
    public GameObject licensesPage;


    private void Start()
    {
        QuestManager.Instance.hasJournal = false;
    }


    public void OnMainMenuButtonPressed()
    {
        LevelManager.Instance.StartLoadingLevel("MainMenu", default, true);
    }

    public void OnLicensesButtonPressed()
    {
        defaultPage.SetActive(false);
        licensesPage.SetActive(true);
    }

    public void OnBackButtonPressed()
    {
        defaultPage.SetActive(true);
        licensesPage.SetActive(false);
    }
}
