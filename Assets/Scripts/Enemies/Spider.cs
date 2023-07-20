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
    //[Header("Spider stats")]
    //[SerializeField] private float PushTimeInSeconds;

    public bool IsResisting { get; private set; }

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
    }

    public void StopResisting()
    {
        IsResisting = false;
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
}
