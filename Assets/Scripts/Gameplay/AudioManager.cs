using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer GameAudioMixer;
    [SerializeField] private FloatVariable MusicVolume;
    [SerializeField] private FloatVariable SoundEffectsVolume;

    public const string MusicVolumeKey = "MusicVolume";
    public const string SFXVolumeKey = "SFXVolume";

    void Start()
    {
        SetInitialSettings();
        SetAudioMixer();
    }

    private void SetInitialSettings()
    {
        CheckKey(MusicVolumeKey, MusicVolume);
        CheckKey(SFXVolumeKey, SoundEffectsVolume);

        SaveSettings();
    }

    private void CheckKey(string key, FloatVariable variable)
    {
        if (variable == null) return;
        if (PlayerPrefs.HasKey(key)) LoadFloatSetting(key, variable);
        else SaveFloatSetting(key, variable.Value);
    }

    private void SaveFloatSetting(string key, float floatValue)
    {
        PlayerPrefs.SetFloat(key, floatValue);
    }

    private void LoadFloatSetting(string key, FloatVariable variable)
    {
        variable.Value = PlayerPrefs.GetFloat(key);
    }

    private void SaveSettings()
    {
        PlayerPrefs.Save();
    }

    private void SetAudioMixer()
    {
        if (GameAudioMixer != null)
        {
            SetAudioMixerFloat(MusicVolumeKey);
            SetAudioMixerFloat(SFXVolumeKey);
        }
    }

    private void SetAudioMixerFloat(string key)
    {
        GameAudioMixer.SetFloat(key, Mathf.Log10(PlayerPrefs.GetFloat(key)) * 20);
    }
}
