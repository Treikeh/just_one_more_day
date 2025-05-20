using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class OptionsPage : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private AudioMixer audioMixer;


    public void SetMasterVolume()
    {
        float volume = masterVolumeSlider.value;
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }
}
