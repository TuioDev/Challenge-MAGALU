using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [Header("Spike info")]
    [SerializeField] private SpikeBehaviour SpikePrefab;
    [SerializeField] private Transform SpikeSpawnTransform;
    [SerializeField] private float SpikeShootCooldown;
    [SerializeField] private float TimeToDisableSpike;
    [Header("Wind info")]
    [SerializeField] private float WindPushCooldown;
    [SerializeField] private GameEvent OnWindPushEvent;
    [SerializeField] private GameEvent OnWindStoppedEvent;

    private Vector3 SpikeSpawnPosition;
    private bool CanPerformAction;
    private bool CanShootSpike;
    private bool CanWindPush;
    private float Timer;

    private void Awake()
    {
        SpikeSpawnPosition = SpikeSpawnTransform.position;
    }

    private void Start()
    {
        CanPerformAction = true;
        CanShootSpike = true;
        CanWindPush = true;
        Timer = 0.0f;
    }

    private void Update()
    {
        CheckCanPerformAction();
    }

    private void CheckCanPerformAction()
    {
        if (CanPerformAction)
        {
            CheckCanShootSpike();
        }
        //Timer += Time.deltaTime;
    }

    #region Spike Related
    // We spawn the spike based on mouse position, and then set it's direction because inside SpikeBehaviour
    // is defined the velocity based on the direction
    public void SpawnSpikeByClick(InputAction.CallbackContext context)
    {
        if (CanShootSpike && context.performed)
        {
            CanShootSpike = false;
            ActivatePooledSpike();
        }
    }

    private Vector2 ScreenToGame2DPosition(Vector2 position)
    {
        return (Vector2)Camera.main.ScreenToWorldPoint(position);
    }

    // Get inactive object and set active, also invoking the disable function from here
    private void ActivatePooledSpike()
    {
        SpikeBehaviour spike = ObjectPool.Instance.GetInactivePooledObject<SpikeBehaviour>();
        if (spike != null)
        {
            spike.transform.position = SpikeSpawnPosition;
            spike.SetDirection(ScreenToGame2DPosition(Mouse.current.position.ReadValue()));
            spike.gameObject.SetActive(true);
            spike.Invoke("DisableObject", TimeToDisableSpike);
        }
    }
    private void CheckCanShootSpike()
    {
        if (!CanShootSpike)
        {
            Timer += Time.deltaTime;
            if (Timer >= SpikeShootCooldown)
            {
                CanShootSpike = true;
                Timer = 0.0f;
            }
        }
    }
    #endregion

    #region Wind Related
    // The second core mechanic is the wind that will call the IsPush function from the IPushable objects

    public void WindPush(InputAction.CallbackContext context)
    {
        if(CanWindPush && context.performed)
        {
            CanWindPush = false;
            PerformIsPushed();
        }
        if (context.canceled)
        {
            StopIsPushed();
            CanWindPush = true;
        }
    }

    private void PerformIsPushed()
    {
        OnWindPushEvent.TriggerEvent();
    }

    private void StopIsPushed()
    {
        OnWindStoppedEvent.TriggerEvent();
    }
    #endregion
}
