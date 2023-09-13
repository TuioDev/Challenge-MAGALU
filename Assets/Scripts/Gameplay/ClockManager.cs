using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class ClockManager : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private SpriteRenderer ClockTimer;
    [Header("Events")]
    [SerializeField] private GameEvent OnTimerEnd;
    [Header("Float reference")]
    [SerializeField] private FloatVariable RoundTime;
    [SerializeField] private FloatVariable LevelTime;

    private float RadialValue = 0f;

    void Start()
    {
        RadialValue = 360 / RoundTime.Value;
    }

    void Update()
    {
        UpdateClockTimerVisual();
    }

    private void UpdateClockTimerVisual()
    {
        ClockTimer.sharedMaterial.SetFloat("_Arc1", LevelTime.Value * RadialValue);
        if (LevelTime.Value > RoundTime.Value)
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
