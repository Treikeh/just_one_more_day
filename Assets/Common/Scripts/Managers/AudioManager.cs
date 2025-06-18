using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public List<AudioSource> tracks = new();
    public AudioSource whiteNoise;


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
        Reset();
    }

    public void Reset()
    {
        for (int i = 0; i < tracks.Count; i++)
        {
            tracks[i].volume = 0f;
        }
        tracks[0].volume = 1f;
        EnableWhiteNoise(false);
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
                StartCoroutine(SwitchTracksWithBlend(tracks[0], tracks[1]));
                break;
            case 4:
                break;
            case 5:
                StartCoroutine(SwitchTracksWithBlend(tracks[1], tracks[2]));
                break;
            case 6:
                break;
            case 7:
                StartCoroutine(SwitchTracksWithBlend(tracks[2], tracks[3]));
                break;
            case 8:
                StartCoroutine(SwitchTracksWithBlend(tracks[3], tracks[4]));
                break;
            case 9:
                break;
            case 10:
                StartCoroutine(SwitchTracksWithBlend(tracks[4], tracks[0]));
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
