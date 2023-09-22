using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : Enemy, IPushable
{
    [Header("Cloud info")]
    [SerializeField] private float ChannelLightningInSeconds;
    public bool IsResisting { get; private set; }
    public bool IsChanneling { get; private set; }

    // Animator parameters
    private const string BOOL_IS_BEING_DAMAGED = "IsBeingDamaged";
    private const string TRIGGER_CHANNELING = "Channeling";
    private const string TRIGGER_LIGHTNING = "Lightning";

    private protected override void ExecuteEnemyBehaviour(Enemy enemy)
    {
        if (IsResisting)
        {
            TakeDamageOrHeal(Time.deltaTime);
        }
        if (IsChanneling)
        {
            // If the cloud is at the end, we don't need to run the logic from behaviour
            return;
        }
        base.ExecuteEnemyBehaviour(enemy);
    }

    public override void ResetEnemy()
    {
        base.ResetEnemy();
        IsResisting = false;
        IsChanneling = false;
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
        IsChanneling = true;
        StartCoroutine(ChannelLighting(base.TriggerOnReachingPie));

    }
    private IEnumerator ChannelLighting(Action TriggerBase)
    {
        EnemyAnimator.SetTrigger(TRIGGER_CHANNELING);

        yield return new WaitForSeconds(ChannelLightningInSeconds);

        EnemyAnimator.SetTrigger(TRIGGER_LIGHTNING);

        TriggerBase();
    }
}
