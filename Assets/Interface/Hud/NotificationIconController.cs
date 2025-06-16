using UnityEngine;

public class NotificationIconController: MonoBehaviour
{
    [SerializeField] private GameObject notificationIcon;
    [SerializeField] private AudioSource audioSource;


    private void OnEnable()
    {
        UiManager.Instance.OnJournalNotification += ShowIcon;
        UiManager.Instance.OnJounralNotificationSeen += HideIcon;
    }
    private void OnDisable(){
        UiManager.Instance.OnJournalNotification -= ShowIcon;
        UiManager.Instance.OnJounralNotificationSeen -= HideIcon;
    }


    private void ShowIcon()
    {
        notificationIcon.SetActive(true);
        audioSource.Play();
    }

    private void HideIcon()
    {
        notificationIcon.SetActive(false);
    }
}
