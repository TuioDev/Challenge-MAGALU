using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [Header("Projectile info")]
    [SerializeField] private ProjectileBehaviour ProjectilePrefab;
    [SerializeField] private Transform ProjectileSpawnTransform;
    [SerializeField] private FloatVariable ProjectileShootCooldown;
    [SerializeField] private float TimeToDisableProjectile;
    [Header("Wind info")]
    [SerializeField] private GameEvent OnWindPushEvent;
    [SerializeField] private GameEvent OnWindStoppedEvent;
    [SerializeField] private GameEvent OnShootProjectile;
    [SerializeField] private ParticleSystem WindVFX;

    private PlayerInput Inputs;
    private Vector3 ProjectileSpawnPosition;
    private bool CanShoot;
    private bool CanWindPush;
    private float Timer;

    private void Awake()
    {
        Inputs = GetComponent<PlayerInput>();
        ProjectileSpawnPosition = ProjectileSpawnTransform.position;
    }

    private void Start()
    {
        CanShoot = true;
        CanWindPush = true;
        Timer = 0.0f;
    }

    private void Update()
    {
        CheckCanShootSpike();
    }

    #region Projectile Related
    // We spawn the spike based on mouse position, and then set it's direction because inside SpikeBehaviour
    // is defined the velocity based on the direction
    public void SpawnProjectileByClick(InputAction.CallbackContext context)
    {
        if (CanShoot && context.performed)
        {
            CanShoot = false;
            OnShootProjectile.TriggerEvent();
            ActivatePooledProjectile();
        }
    }

    private Vector2 ScreenToGame2DPosition(Vector2 position)
    {
        return (Vector2)Camera.main.ScreenToWorldPoint(position);
    }

    // Get inactive object and set active, also invoking the disable function from here
    private void ActivatePooledProjectile()
    {
        ProjectileBehaviour projectile = ObjectPool.Instance.GetInactivePooledObject<ProjectileBehaviour>();

        if (projectile != null)
        {
            projectile.transform.position = ProjectileSpawnPosition;
            projectile.SetDirection(ScreenToGame2DPosition(Mouse.current.position.ReadValue()));
            projectile.gameObject.SetActive(true);
            projectile.Invoke("DisableObject", TimeToDisableProjectile);
        }
    }
    private void CheckCanShootSpike()
    {
        if (!CanShoot)
        {
            Timer += Time.deltaTime;
            if (Timer >= ProjectileShootCooldown.Value)
            {
                CanShoot = true;
                Timer = 0.0f;
            }
        }
    }
    #endregion

    #region Wind Related
    // The second core mechanic is the wind, it will raise an event and objects subscribed will act

    public void WindPush(InputAction.CallbackContext context)
    {
        if (CanWindPush && context.performed)
        {
            CanWindPush = false;
            PerformIsPushed();
        }
        if (!CanWindPush && context.canceled)
        {
            StopIsPushed();
            CanWindPush = true;
        }
    }

    private void PerformIsPushed()
    {
        OnWindPushEvent.TriggerEvent();
        WindVFX.Play();
    }

    private void StopIsPushed()
    {
        OnWindStoppedEvent.TriggerEvent();
        WindVFX.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }
    #endregion

    #region Action Map
    public void ChangeToUIActionMap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Inputs.SwitchCurrentActionMap("UI");
        }
    }

    public void ChangeToGameplayActionMap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Inputs.SwitchCurrentActionMap("Gameplay");
        }
    }
    #endregion
}
