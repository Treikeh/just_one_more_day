using UnityEngine;

public class PuzzlePickupObject : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private AudioClip dropSound;


    public void PickUp()
    {
        audioSource.PlayOneShot(pickupSound);
    }

    public void Drop()
    {
        audioSource.PlayOneShot(dropSound);
    }
}
