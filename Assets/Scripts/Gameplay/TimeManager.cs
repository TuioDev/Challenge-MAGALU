using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeManager : MonoBehaviour
{
    [Header("Float reference")]
    [SerializeField] private FloatVariable LevelTime;

    void Awake()
    {
        ResumeGame();
        SetLevelTime();
        //DisableObject();
    }

    void Update()
    {
        UpdateLevelTime();
    }

    private void DisableObject()
    {
        this.gameObject.SetActive(false);
    }

    private void SetLevelTime()
    {
        if (LevelTime != null) LevelTime.Value = 0f;
    }

    private void UpdateLevelTime()
    {
        if (LevelTime != null) LevelTime.Value += Time.deltaTime;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void PauseAudio()
    {
        AudioListener.pause = true;
    }

    public void ResumeAudio()
    {
        AudioListener.pause = false;
    }
}
