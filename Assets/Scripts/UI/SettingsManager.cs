using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Volume References")]
    [Header("Music")]
    [SerializeField] private FloatVariable MusicVolume;
    [SerializeField] private Slider SliderMusicVolume;
    [SerializeField] private TextMeshProUGUI TextMusicVolume;
    [Header("Sound effects")]
    [SerializeField] private FloatVariable SoundEffectsVolume;
    [SerializeField] private Slider SliderSoundEffectsVolume;
    [SerializeField] private TextMeshProUGUI TextSoundEffectsVolume;

    void Start()
    {
        SetTexts();
    }

    private void SetTexts()
    {
        // Music
        SliderMusicVolume.value = MusicVolume.Value * 10;
        TextMusicVolume.text = MusicVolume.Value.ToString("0.0");

        SliderSoundEffectsVolume.value = SoundEffectsVolume.Value * 10;
        TextSoundEffectsVolume.text = SoundEffectsVolume.Value.ToString("0.0");
    }

    public void ChangeMusicVolume(float value)
    {
        value /= 10;
        MusicVolume.Value = value;
        TextMusicVolume.text = value.ToString("0.0");
    }

    public void ChangeSoundEffectsVolume(float value)
    {
        value /= 10;
        SoundEffectsVolume.Value = value;
        TextSoundEffectsVolume.text = value.ToString("0.0");
    }
}
