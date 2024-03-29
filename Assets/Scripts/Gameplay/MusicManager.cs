using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Musics managed by this")]
    [SerializeField] private AudioClip MusicMenu;
    [SerializeField] private AudioClip MusicGameplay;

    private static MusicManager Instance;
    private AudioSource MusicPlayer;

    private void Awake()
    {
        SetInstance();

        MusicPlayer = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SetMusic;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SetMusic;
    }

    private void SetInstance()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(this.gameObject); }
        else { Destroy(this.gameObject); }
    }

    private void SetMusic(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case GameManager.SCENE_MENU:
            case GameManager.SCENE_LEVEL_SELECTION:
            case GameManager.SCENE_CONTROLS:
            case GameManager.SCENE_SETTINGS:
            case GameManager.SCENE_CREDITS:
                PlayMusic(MusicMenu);
                break;
            default:
                PlayMusic(MusicGameplay);
                break;
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (MusicPlayer.clip == clip) return;

        MusicPlayer.clip = clip;
        MusicPlayer.loop = true;
        MusicPlayer.Play();
    }
}
