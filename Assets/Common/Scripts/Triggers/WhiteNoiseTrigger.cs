using UnityEngine;

public class WhiteNoiseTrigger : MonoBehaviour
{
    [SerializeField] private bool startNoise = true;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AudioManager.Instance.EnableWhiteNoise(startNoise);
        }
    }
}
