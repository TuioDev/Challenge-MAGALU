using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private FloatVariable Volume;

    private AudioSource MusicPlayer;

    private void Awake()
    {
        MusicPlayer = GetComponent<AudioSource>();
    }
    void Start()
    {

    }

    public void PlayMusic(AudioClip clip)
    {
        MusicPlayer.clip = clip;
        MusicPlayer.volume = Volume.Value;
        MusicPlayer.loop = true;
        MusicPlayer.Play();
    }
}
