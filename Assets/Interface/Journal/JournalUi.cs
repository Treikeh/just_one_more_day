using UnityEngine;

public class JournalUi : MonoBehaviour
{
    [SerializeField] private GameObject journalPanel;

    private void OnEnable()
    {
        GameEventManager.Instance.inputEvents.onJournalPressed += JounralPressed;
        GameEventManager.Instance.inputEvents.onCancelPressed += CancelPressed;
    }

    private void OnDisable()
    {
        GameEventManager.Instance.inputEvents.onJournalPressed -= JounralPressed;
        GameEventManager.Instance.inputEvents.onCancelPressed -= CancelPressed;
    }


    // Hide Journal
    public void CancelPressed()
    {
        if (journalPanel.activeInHierarchy)
        {
            journalPanel.SetActive(false);
            GameEventManager.Instance.inputEvents.ActionMapChanged("Player");
        }
    }

    // Show Journal
    private void JounralPressed()
    {
        if (!journalPanel.activeInHierarchy)
        {
            journalPanel.SetActive(true);
            GameEventManager.Instance.inputEvents.ActionMapChanged("Ui");
        }
    }
}
