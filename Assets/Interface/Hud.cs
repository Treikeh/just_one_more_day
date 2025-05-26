using System.Collections;
using UnityEngine;


public class Hud : MonoBehaviour
{
    [SerializeField] private Animator notificationAnimator;

    private float notificationDisplayDuration = 2f;
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
        notificationAnimator.SetBool("isShown", true);
        yield return new WaitForSeconds(notificationDisplayDuration);
        notificationAnimator.SetBool("isShown", false);
    }
}


