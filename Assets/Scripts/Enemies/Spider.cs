using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// The functions are called in the Game Event Listener
[RequireComponent(typeof(GameEventListener))]
public class Spider : Enemy, IDamageable, IPushable
{
    [Header("Spider info")]
    
    [SerializeField] private float WebTimeToSpawn;
    [SerializeField] private float WaitTimeAfterWebSpawn;
    [field: SerializeField] public float MinimumDamageToPlayAnimation { get; set; }

    public bool IsResisting { get; private set; }

    // Animator parameters
    private static readonly int BOOL_IS_BEING_PUSHED = Animator.StringToHash("IsBeingPushed");
    private static readonly int TRIGGER_TOOKDAMAGE = Animator.StringToHash("TookDamage");
    private static readonly int ANIMATION_SIDER_DAMAGE = Animator.StringToHash("Spider_Damage");

    public int GetEnemyHealthPercentage() => 0;

    private protected override void ExecuteEnemyBehaviour(Enemy enemy)
    {
        if (IsResisting)
        {
            TakeDamageOrHeal(Time.deltaTime);
        }
        base.ExecuteEnemyBehaviour(enemy);
    }

    public override void ResetEnemy()
    {
        base.ResetEnemy();
        IsResisting = false;
    }

    public void BeginResisting()
    {
        Debug.Log("Começou");
        IsResisting = true;
        EnemyAnimator.SetBool(BOOL_IS_BEING_PUSHED, true);
    }

    public void StopResisting()
    {
        IsResisting = false;
        EnemyAnimator.SetBool(BOOL_IS_BEING_PUSHED, false);
        Debug.Log("Parou");
    }
    public void TakeDamageOrHeal(float damage)
    {
        // Decrease health based on time
        EnemyHealth.TakeDamage(damage);

        // If health is bellow zero, disable enemy
        if (EnemyHealth.CurrentAmount <= 0)
        {
            DisableObject();
        }

        if (damage >= MinimumDamageToPlayAnimation)
        {
            EnemyAnimator.SetTrigger(TRIGGER_TOOKDAMAGE);
        }
    }

    public override void TriggerOnReachingPie()
    {
        RemoveMyselfFromPathList();
        base.TriggerOnReachingPie();
    }

    public override void DisableObject()
    {
        RemoveMyselfFromPathList();
        base.DisableObject();
    }

    private void RemoveMyselfFromPathList()
    {
        if (EnemyPath == null) return;
        EnemyPath.gameObject.GetComponent<EnemiesReferenceKeeper>().RemoveEnemyOnPath(this);
    }
}
