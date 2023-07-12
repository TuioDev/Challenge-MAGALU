using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ant : Enemy, IDamageable
{
    [Header("Ant info")]
    [SerializeField] private protected GameEvent OnTakingDamageEvent;
    [Header("Ant status")]
    [SerializeField] private protected int MaxHealth;

    private protected Health EnemyHealth = new();
    public int GetEnemyHealthPercentage() => EnemyHealth.CurrentAmount * 100 / MaxHealth;

    public void TakeDamageOrHeal(int damage)
    {
        // Send the damage amount to this enemy health
        EnemyHealth.TakeDamage(damage);

        // Check if the enemy is going to die, disable object and return
        if (EnemyHealth.CurrentAmount <= 0)
        {
            DisableObject();
            return;
        }

        // Trigger if survived the damage
        TriggerOnTakingDamage();
    }

    public override void ResetEnemy()
    {
        base.ResetEnemy();
        EnemyHealth.SetAmount(MaxHealth);
    }


    public void TriggerOnTakingDamage()
    {
        OnTakingDamageEvent.TriggerEvent();
    }
}
