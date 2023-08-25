using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class ClockManager : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private SpriteRenderer ClockTimer;
    [SerializeField] private float RoundTime;
    [Header("Events")]
    [SerializeField] private GameEvent OnTimerEnd;
    [Header("Float reference")]
    [SerializeField] private FloatVariable LevelTime;

    private float RadialValue = 0f;

    void Start()
    {
        RadialValue = 360 / RoundTime;
    }

    void Update()
    {
        UpdateClockTimerVisual();
    }

    private void UpdateClockTimerVisual()
    {
        ClockTimer.sharedMaterial.SetFloat("_Arc1", LevelTime.Value * RadialValue);
        if (LevelTime.Value > RoundTime)
        {
            ClockTimer.sharedMaterial.SetFloat("_Arc1", 360f);
            TriggerTimerEnd();
            this.enabled = false;
        }
    }

    private void TriggerTimerEnd()
    {
        OnTimerEnd.TriggerEvent();
    }
}
