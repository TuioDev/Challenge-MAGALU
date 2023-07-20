using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviour/Change based on Health", fileName = "ChangeBasedOnHealth")]
public class ChangeBasedOnHealth : AIBehaviour
{
    [Header("Change Based on Health")]
    [SerializeField][Range(1, 99)] private int HealthPercentageTrigger;

    // This is called with the Game Event Listener with OnTakeDamage
    public void CheckHealthAndTrigger(Enemy enemy)
    {
        if(enemy is IDamageable && IsHealthBellowTrigger(enemy as IDamageable))
        {
            DesignatedEvent.RemoveListener(enemy.GetComponent<GameEventListener>());
            ChangeCurrentBrain(enemy, SecondBrain);
        }
    }

    // Using interface as a parameter is not good, but it's working
    private bool IsHealthBellowTrigger(IDamageable damageable)
    {
        return damageable.GetEnemyHealthPercentage() <= HealthPercentageTrigger;
    }
}
