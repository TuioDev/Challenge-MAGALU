using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [Header("General audio")]
    [SerializeField] private FloatVariable SFXVolume;
    [Header("SFX clips")]
    [SerializeField] private AudioClip EnemyDamaged;
    [SerializeField] private AudioClip ReachingPie;

    private Vector3 CameraPosition;
    private AudioSource SFXPlayer;

    void Start()
    {
        SFXPlayer = GetComponent<AudioSource>();
        CameraPosition = Camera.main.transform.position;
    }

    public void PlaySFX(AudioCollection audios)
    {
        AudioSource source = PlayClipAtPoint(audios.GetRandomAudioClip(), CameraPosition, SFXVolume.Value);
        source.pitch = audios.GetRandomPitch();
    }

    // Smae method from AudioSource but return an AudioSource to edit the pitch
    private AudioSource PlayClipAtPoint(AudioClip clip, Vector3 position, [UnityEngine.Internal.DefaultValue("1.0F")] float volume)
    {
        GameObject gameObject = new GameObject("One shot audio");
        gameObject.transform.position = position;
        AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
        audioSource.clip = clip;
        audioSource.spatialBlend = 1f;
        audioSource.volume = volume;
        audioSource.Play();
        Object.Destroy(gameObject, clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale));
        return audioSource;
    }
}

