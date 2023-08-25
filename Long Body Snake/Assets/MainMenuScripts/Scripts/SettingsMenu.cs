using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider MusicSlider;
    public Slider SfxSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }

        if (PlayerPrefs.HasKey("SfxVolume"))
        {
            LoadSfxVolume();
        }
    }


    public void SetValueMusicVol(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetValueSFXVol(float volume)
    {
        audioMixer.SetFloat("SFX Volume", volume);
        PlayerPrefs.SetFloat("SfxVolume", volume);
    }

    private void LoadVolume()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetValueMusicVol(PlayerPrefs.GetFloat("musicVolume"));
    }

    private void LoadSfxVolume()
    {
        SfxSlider.value = PlayerPrefs.GetFloat("SfxVolume");
        SetValueSFXVol(PlayerPrefs.GetFloat("SfxVolume"));
    }
}
