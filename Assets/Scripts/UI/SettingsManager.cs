using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private AudioMixer GameAudioMixer;
    [Header("Toogle References")]
    [SerializeField] private Toggle ToggleMusicMuted;
    [SerializeField] private Toggle ToggleSFXMuted;
    [Header("Bool variables")]
    [SerializeField] private BoolVariable IsMusicMuted;
    [SerializeField] private BoolVariable IsSFXMuted;
    [Header("Float variables")]
    [SerializeField] private FloatVariable MusicVolumeBefore;
    [SerializeField] private FloatVariable SFXVolumeBefore;
    [Header("Volume References")]
    [Header("Music")]
    [SerializeField] private FloatVariable MusicVolume;
    [SerializeField] private Slider SliderMusicVolume;
    [SerializeField] private TextMeshProUGUI TextMusicVolume;
    [Header("Sound effects")]
    [SerializeField] private FloatVariable SoundEffectsVolume;
    [SerializeField] private Slider SliderSoundEffectsVolume;
    [SerializeField] private TextMeshProUGUI TextSoundEffectsVolume;

    private float MusicVolumeBeforeMute; // remove
    private float SFXVolumeBeforeMute; // remove

    public const string MusicVolumeKey = "MusicVolume"; // remove
    public const string SoundEffectsVolumeKey = "SFXVolume";
    public const string MusicMutedKey = "MusicMuted"; // remove
    public const string SoundEffectsMutedKey = "SFXMuted";

    void Start()
    {
        SetVolumes();
    }

    private void SetVolumes()
    {
        // Music
        //SetToggleInfo(ToggleMusicMuted, IsMusicMuted);
        SetSliderInfo(MusicVolumeKey, SliderMusicVolume, TextMusicVolume);
        // SFX
        //SetToggleInfo(ToggleSFXMuted, IsSFXMuted);
        SetSliderInfo(SoundEffectsVolumeKey, SliderSoundEffectsVolume, TextSoundEffectsVolume);
    }

    private void SetSliderInfo(string key, Slider slider, TextMeshProUGUI text)
    {
        float volume = PlayerPrefs.GetFloat(key);
        slider.value = volume * 10;
        text.text = volume.ToString("0.0");
        GameAudioMixer.SetFloat(key, Mathf.Log10(volume) * 20);
    }

    //private void SetToggleInfo(Toggle toggle, BoolVariable variable)
    //{
    //    toggle.isOn = variable.Value;
    //}

    // The slider changes from 0 to 10 so that it keeps the whole numbers aspect
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

    //public void MuteMusic(bool mute)
    //{
    //    if (mute)
    //    {
    //        MusicVolumeBefore.Value = PlayerPrefs.GetFloat(MusicVolumeKey);
    //        ChangeMusicVolume(0f);
    //        SliderMusicVolume.value = 0f;
    //        IsMusicMuted.Value = true;
    //    }
    //    else
    //    {
    //        ChangeMusicVolume(MusicVolumeBefore.Value * 10);
    //        SliderMusicVolume.value = MusicVolumeBefore.Value * 10;
    //        IsMusicMuted.Value = false;
    //    }
    //}

    //public void MuteSFX(bool mute)
    //{
    //    if (mute)
    //    {
    //        SFXVolumeBefore.Value = PlayerPrefs.GetFloat(SoundEffectsVolumeKey);
    //        ChangeSoundEffectsVolume(0f);
    //        SliderSoundEffectsVolume.value = 0f;
    //        IsSFXMuted.Value = true;
    //    }
    //    else
    //    {
    //        ChangeSoundEffectsVolume(SFXVolumeBefore.Value * 10);
    //        SliderSoundEffectsVolume.value = SFXVolumeBefore.Value * 10;
    //        IsSFXMuted.Value = false;
    //    }
    //}
}
