using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private AudioMixer GameAudioMixer;
    [Header("Volume References")]
    [Header("Music")]
    [SerializeField] private FloatVariable MusicVolume;
    [SerializeField] private Slider SliderMusicVolume;
    [SerializeField] private TextMeshProUGUI TextMusicVolume;
    [Header("Sound effects")]
    [SerializeField] private FloatVariable SoundEffectsVolume;
    [SerializeField] private Slider SliderSoundEffectsVolume;
    [SerializeField] private TextMeshProUGUI TextSoundEffectsVolume;

    public const string MusicVolumeKey = "MusicVolume";
    public const string SoundEffectsVolumeKey = "SFXVolume";

    void Start()
    {
        SetVolumes();
    }

    private void SetVolumes()
    {
        // Music
        SetObjectInfo(MusicVolumeKey, SliderMusicVolume, TextMusicVolume);
        // SFX
        SetObjectInfo(SoundEffectsVolumeKey, SliderSoundEffectsVolume, TextSoundEffectsVolume);
    }

    private void SetObjectInfo(string key, Slider slider, TextMeshProUGUI text)
    {
        float volume = PlayerPrefs.GetFloat(key);
        slider.value = volume * 10;
        text.text = volume.ToString("0.0");
        GameAudioMixer.SetFloat(key, Mathf.Log10(volume) * 20);
    }

    public void ChangeMusicVolume(float value)
    {
        if (value == 0) value = 0.0001f;
        else value /= 10;

        MusicVolume.Value = value;
        TextMusicVolume.text = value.ToString("0.0");
        GameAudioMixer.SetFloat(MusicVolumeKey, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(MusicVolumeKey, value);

        PlayerPrefs.Save();
    }

    public void ChangeSoundEffectsVolume(float value)
    {
        if (value == 0) value = 0.0001f;
        else value /= 10;

        SoundEffectsVolume.Value = value;
        TextSoundEffectsVolume.text = value.ToString("0.0");
        GameAudioMixer.SetFloat(SoundEffectsVolumeKey, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(SoundEffectsVolumeKey, value);

        PlayerPrefs.Save();
    }
}
