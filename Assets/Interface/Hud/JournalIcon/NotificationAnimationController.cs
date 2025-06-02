using System.Collections;
using UnityEngine;

public class NotificationAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Coroutine notificationCoroutine;


    private void OnEnable() { UiManager.Instance.OnJournalNotification += JournalNotification; }
    private void OnDisable() { UiManager.Instance.OnJournalNotification -= JournalNotification; }


    // TODO: Have different notification icons for Quest and Character notifications
    private void JournalNotification()
    {
        if (notificationCoroutine != null)
        {
            StopCoroutine(notificationCoroutine);
        }

        notificationCoroutine = StartCoroutine(JournalNotificationSequence());
    }

    private IEnumerator JournalNotificationSequence()
    {
        animator.SetBool("isOpen", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isOpen", false);
    }
}
