using System.Collections;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource track_1;
    [SerializeField] private AudioSource track_3;
    [SerializeField] private AudioSource track_2;
    [SerializeField] private AudioSource track_4;
    [SerializeField] private AudioSource whiteNoise;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void OnEnable() { QuestManager.Instance.OnStoryProgressChanged += OnStoryProgressChanged; }

    public void OnDisable() { QuestManager.Instance.OnStoryProgressChanged -= OnStoryProgressChanged; }


    public void Start()
    {
        track_1.volume = 1;
        track_2.volume = 0;
        track_3.volume = 0;
        track_4.volume = 0;
    }


    private void OnStoryProgressChanged(int storyProgress)
    {
        // Switch music tracks when the story reaches certain points
        switch (storyProgress)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                StartCoroutine(SwitchTracksWithBlend(track_1, track_2));
                break;
            case 4:
                break;
            case 5:
                StartCoroutine(SwitchTracksWithBlend(track_2, track_3));
                break;
            case 6:
                break;
            case 7:
                StartCoroutine(SwitchTracksWithBlend(track_3, track_4));
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                StartCoroutine(SwitchTracksWithBlend(track_4, track_1));
                break;
            
        }
    }

    private IEnumerator SwitchTracksWithBlend(AudioSource oldTrack, AudioSource newTrack)
    {
        float WaitTime = 0.15f;
        while (oldTrack.volume > 0f)
        {
            // Reduce volume of old track
            oldTrack.volume -= 0.1f;
            // Set the volume of the new track = to what's missing from the volume of the old track
            // This ensures that the total precived volume stays the same
            newTrack.volume = 1f - oldTrack.volume;
            yield return new WaitForSeconds(WaitTime);
        }
    }

    public void EnableWhiteNoise(bool enable)
    {
        if (enable)
        {
            whiteNoise.Play();
        }
        else
        {
            whiteNoise.Stop();
        }
    }
}
