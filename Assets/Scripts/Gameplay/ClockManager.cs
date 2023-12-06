using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class ClockManager : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private SpriteRenderer ClockTimer;
    [Header("Events")]
    [SerializeField] private GameEvent OnTimerEnd;
    [SerializeField] private GameEvent OnCountdown;
    [SerializeField] private GameEvent OnAlmostEnding;
    [Header("Level info")]
    [SerializeField] private FloatVariable RoundTime;
    [SerializeField] private FloatVariable LevelTime;
    [Header("Timers info")]
    [SerializeField] private FloatVariable Countdown;
    [SerializeField] private FloatVariable AlmostEnding;

    private float RadialValue = 0f;
    private bool IsCountdown;
    private bool IsAlmostEnding;

    void Start()
    {
        RadialValue = 360 / RoundTime.Value;
        IsCountdown = false;
        IsAlmostEnding = false;
        ClockTimer.sharedMaterial.SetFloat("_Arc1", 0);
    }

    void Update()
    {
        UpdateClockTimer();
    }

    private void UpdateClockTimer()
    {
        ClockTimer.sharedMaterial.SetFloat("_Arc1", LevelTime.Value * RadialValue);

        // Check if the it's on countdown
        CheckIsCountdown();

        // Check if it's almost ending
        CheckAlmostEnding();

        // Check if the timer ended
        if (LevelTime.Value > RoundTime.Value)
        {
            ClockTimer.sharedMaterial.SetFloat("_Arc1", 360f);
            this.enabled = false;
            TriggerTimerEnd();
        }
    }

    private void CheckIsCountdown()
    {
        if (RoundTime.Value - LevelTime.Value < Countdown.Value && !IsCountdown)
        {
            IsCountdown = true;
            OnCountdown.TriggerEvent();
        }
    }

    private void CheckAlmostEnding()
    {
        if (IsCountdown && RoundTime.Value - LevelTime.Value < AlmostEnding.Value && !IsAlmostEnding)
        {
            IsAlmostEnding = true;
            OnAlmostEnding.TriggerEvent();
        }
    }

    private void TriggerTimerEnd()
    {
        OnTimerEnd.TriggerEvent();
    }
}
