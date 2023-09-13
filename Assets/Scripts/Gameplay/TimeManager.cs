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

    public void PlusFiveSeconds(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            LevelTime.Value -= 5f;
        }
    }
}
