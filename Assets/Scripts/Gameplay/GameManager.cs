using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer ClockTimer;
    [SerializeField] private float RoundTime;

    private float RadialValue = 0f;
    private float ElapsedTime = 0f;

    // Maybe try using ScriptableOject events
    //public static event Action OnGameOver;
    // Start is called before the first frame update
    void Start()
    {
        RadialValue = 360 / RoundTime;
    }

    // Update is called once per frame
    void Update()
    {
        ElapsedTime += Time.deltaTime;
        ClockTimer.sharedMaterial.SetFloat("_Arc1", ElapsedTime * RadialValue);
        if (ElapsedTime > RoundTime)
        {
            ClockTimer.sharedMaterial.SetFloat("_Arc1", 0f);
            this.enabled = false;
        }
    }
}
