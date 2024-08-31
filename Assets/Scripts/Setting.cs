using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;

    private const string BGM_VOLUME_KEY = "BGMVolume";
    private const string SFX_VOLUME_KEY = "SFXVolume";

    void Start()
    {
        // Get the volume values from PlayerPrefs, defaulting to 1 if not found
        float bgmVolume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 1f);

        // Set the sliders to the retrieved values
        bgmSlider.value = bgmVolume;
        sfxSlider.value = sfxVolume;
    }

    void Update()
    {
        // Update PlayerPrefs with the slider values every frame
        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, bgmSlider.value);
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, sfxSlider.value);

        // Optional: Apply the volume settings to your audio sources
        // For example:
        // AudioManager.Instance.SetBGMVolume(bgmSlider.value);
        // AudioManager.Instance.SetSFXVolume(sfxSlider.value);
    }
}
