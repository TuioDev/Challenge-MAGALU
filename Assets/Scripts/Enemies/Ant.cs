using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ant : Enemy, IDamageable
{
    [Header("Ant info")]
    [SerializeField] private protected GameEvent OnTakingDamageEvent;

    public int GetEnemyHealthPercentage() => (int)(EnemyHealth.CurrentAmount * 100 / MaxHealth);

    public void TakeDamageOrHeal(float damage)
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

    public void TriggerOnTakingDamage()
    {
        OnTakingDamageEvent.TriggerEvent();
    }
}
