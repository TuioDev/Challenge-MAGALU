using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

// Rename this to a TimeManager
public class GameManager : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private SpriteRenderer ClockTimer;
    [SerializeField] private float RoundTime;
    [Header("Events")]
    [SerializeField] private GameEvent OnTimerEnd;

    private float RadialValue = 0f;
    private float ElapsedTime = 0f;

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
        ElapsedTime += Time.deltaTime;
        ClockTimer.sharedMaterial.SetFloat("_Arc1", ElapsedTime * RadialValue);
        if (ElapsedTime > RoundTime)
        {
            ClockTimer.sharedMaterial.SetFloat("_Arc1", 360f);
            TriggerTimerEnd();
            this.enabled = false;
        }
    }

    public void TriggerTimerEnd()
    {
        OnTimerEnd.TriggerEvent();
    }
}
