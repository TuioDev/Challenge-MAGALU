using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup SFXAudioMixer;

    private Vector3 CameraPosition;

    void Start()
    {
        CameraPosition = Camera.main.transform.position;
    }

    public void PlaySFX(AudioCollection audios)
    {
        AudioSource source = PlayClipAtPoint(audios.GetRandomAudioClip(), CameraPosition);
        source.pitch = audios.GetRandomPitch();
    }

    // Same method from AudioSource but return an AudioSource to edit the pitch
    private AudioSource PlayClipAtPoint(AudioClip clip, Vector3 position, [UnityEngine.Internal.DefaultValue("1.0F")] float volume = 1.0f)
    {
        GameObject gameObject = new("One shot audio");
        gameObject.transform.position = position;
        AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = SFXAudioMixer;
        audioSource.spatialBlend = 0f;
        audioSource.volume = volume;
        audioSource.Play();
        Object.Destroy(gameObject, clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale));
        return audioSource;
    }
}

