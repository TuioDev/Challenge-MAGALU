using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// The functions are called in the Game Event Listener
[RequireComponent(typeof(GameEventListener))]
public class Spider : Enemy, IDamageable, IPushable
{
    [Header("Spider info")]
    [SerializeField] private GameEvent OnBeingPushed;
    [Header("Spiderweb info")]
    [SerializeField] private float WebTimeToSpawn;
    [SerializeField] private float WaitTimeAfterWebSpawn;

    public bool IsResisting { get; private set; }

    // Animator parameters
    private const string BOOL_IS_BEING_DAMAGED = "IsBeingDamaged";
    private const string TRIGGER_PROJECTILEDAMAGE = "ProjectileDamage";

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
        IsResisting = true;
        EnemyAnimator.SetBool(BOOL_IS_BEING_DAMAGED, true);
    }

    public void StopResisting()
    {
        IsResisting = false;
        EnemyAnimator.SetBool(BOOL_IS_BEING_DAMAGED, false);
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
