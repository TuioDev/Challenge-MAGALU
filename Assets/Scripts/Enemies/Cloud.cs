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
    public bool ChannelingWasInvoked { get; private set; }

    private protected override void ExecuteEnemyBehaviour(Enemy enemy)
    {
        if (IsResisting)
        {
            TakeDamageOrHeal(Time.deltaTime);
        }
        if (IsChanneling)
        {
            // If the cloud is at the end, we don't need to run all the logic from behaviour
            return;
        }
        base.ExecuteEnemyBehaviour(enemy);
    }

    public override void ResetEnemy()
    {
        base.ResetEnemy();
        IsResisting = false;
        IsChanneling = false;
        ChannelingWasInvoked = false;
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

    public override void TriggerOnReachingPie()
    {
        // TODO: Change sprite/animation
        IsChanneling = true;
        StartCoroutine(ChannelLighting(base.TriggerOnReachingPie));

    }
    private IEnumerator ChannelLighting(Action TriggerBase)
    {
        EnemyAnimator.SetTrigger("Channeling");

        yield return new WaitForSeconds(ChannelLightningInSeconds);

        TriggerBase();

        //DisableObject(); // The animation has a DisableObject event in the end
    }
}
