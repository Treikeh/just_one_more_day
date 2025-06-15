using UnityEngine;

public class FootstepsSounds : MonoBehaviour
{
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private AudioClip fotstep01;
    [SerializeField] private AudioClip fotstep02;


    public void PlayFootstepSound()
    {
        bool use02 = Random.value > 0.5f;
        footstepSource.PlayOneShot(use02 == true ? fotstep01: fotstep02);
    }
}
