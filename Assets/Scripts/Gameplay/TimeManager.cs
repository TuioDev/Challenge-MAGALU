using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeManager : MonoBehaviour
{
    [Header("Float reference")]
    [SerializeField] private FloatVariable RoundTime;
    [SerializeField] private FloatVariable LevelTime;

    void Awake()
    {
        UnpauseGame();
        LevelTime.Value = 0f;
    }

    void Update()
    {
        UpdateLevelTime();
    }

    private void UpdateLevelTime()
    {
        LevelTime.Value += Time.deltaTime;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }
}
