using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviour/Change based on Health", fileName = "ChangeBasedOnHealth")]
public class ChangeBasedOnHealth : AIBehaviour
{
    [Header("Brains")]
    [SerializeField] private Brain NormalMovement;
    [SerializeField] private Brain FastMovement;

    [Header("Change Based on Health")]
    [SerializeField][Range(1, 99)] private int HealthPercentageTrigger;
    [SerializeField] private GameEvent OnTakingDamageEvent;
    

    public override void Initialize(Enemy enemy)
    {
        enemy.SetCurrentBrain(NormalMovement);
    }

    public override void Execute(Enemy enemy)
    {
        enemy.GetCurrentBrain().Think(enemy);
    }

    public void CheckHealthAndTrigger(Enemy enemy)
    {
        if(enemy is IDamageable && IsHealthBellowTrigger(enemy as IDamageable))
        {
            OnTakingDamageEvent.RemoveListener(enemy.GetComponent<GameEventListener>());
            ChangeCurrentBrain(enemy, FastMovement);
        }
    }

    // This is very suspicious, but it works
    private bool IsHealthBellowTrigger(IDamageable damageable)
    {
        return damageable.GetEnemyHealthPercentage() <= HealthPercentageTrigger;
    }
}
