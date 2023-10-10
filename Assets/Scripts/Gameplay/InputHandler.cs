using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class InputHandler : MonoBehaviour
{
    [Header("Browser info")]
    [SerializeField] private BoolVariable IsMobileBrowser;
    [SerializeField] private GameObject MobileButton;
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

    private Vector2 TouchPosition;
    private bool CanShoot;
    private bool CanWindPush;
    private float Timer;

    private const string ACTIONMAP_GAMEPLAY = "Gameplay";
    private const string ACTIONMAP_UI = "UI";
    private const string ACTIONMAP_PAUSE = "Pause";
    private const string ACTIONMAP_DISABLED = "Disabled";

    private void Awake()
    {
        Inputs = GetComponent<PlayerInput>();
        ProjectileSpawnPosition = ProjectileSpawnTransform.position;
        MobileControls();
    }

    private void Start()
    {
        TouchPosition = Vector2.zero;
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
            ShootProjectile(ScreenToGame2DPosition(Mouse.current.position.ReadValue()));
        }
    }

    public void SpawnProjectileByTouch()
    {
        if (CanShoot)
        {
            ShootProjectile(TouchPosition);
        }
    }

    private void ShootProjectile(Vector2 inputPosition)
    {
        CanShoot = false;
        OnShootProjectile.TriggerEvent();
        ActivatePooledProjectile(inputPosition);
    }

    private Vector2 ScreenToGame2DPosition(Vector2 position)
    {
        return (Vector2)Camera.main.ScreenToWorldPoint(position);
    }

    // Get inactive object and set active, also invoking the disable function from here
    private void ActivatePooledProjectile(Vector2 inputPosition)
    {
        ProjectileBehaviour projectile = ObjectPool.Instance.GetInactivePooledObject<ProjectileBehaviour>();

        if (projectile != null)
        {
            projectile.transform.position = ProjectileSpawnPosition;
            projectile.SetDirection(inputPosition);
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

    #region Mobile Related

    private void MobileControls()
    {
        MobileButton.SetActive(IsMobileBrowser.Value);
        EnhancedTouchSupport.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp += SetTouchPosition;
    }

    private void SetTouchPosition(Finger finger)
    {
        TouchPosition = ScreenToGame2DPosition(finger.currentTouch.screenPosition);
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

    public void PerformIsPushed()
    {
        OnWindPushEvent.TriggerEvent();
        WindVFX.Play();
    }

    public void StopIsPushed()
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
            Inputs.SwitchCurrentActionMap(ACTIONMAP_UI);
        }
    }

    public void ChangeToUIActionMap()
    {
        Inputs.SwitchCurrentActionMap(ACTIONMAP_UI);
    }

    public void ChangeToGameplayActionMap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Inputs.SwitchCurrentActionMap(ACTIONMAP_GAMEPLAY);
        }
    }
    public void ChangeToGameplayActionMap()
    {
        Inputs.SwitchCurrentActionMap(ACTIONMAP_GAMEPLAY);
    }

    public void ChangeToPauseActionMap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Inputs.SwitchCurrentActionMap(ACTIONMAP_PAUSE);
        }
    }

    public void ChangeToPauseActionMap()
    {
        Inputs.SwitchCurrentActionMap(ACTIONMAP_PAUSE);
    }

    public void ChangeToDisabledActionMap()
    {
        Inputs.SwitchCurrentActionMap(ACTIONMAP_DISABLED);
    }
    #endregion
}
