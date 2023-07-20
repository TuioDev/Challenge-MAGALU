using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviour/Change based on Time", fileName = "ChangeBasedOnTime")]
public class ChangeBasedOnTime : AIBehaviour
{
    [Header("Change Based on Time")]
    [SerializeField][Range(1, 30)] private int TimeInSecondsToTrigger;

    public override void Execute(Enemy enemy)
    {
        if (enemy.GetTimePosition() >= TimeInSecondsToTrigger)
        {
            DesignatedEvent.RemoveListener(enemy.GetComponent<GameEventListener>());
            ChangeCurrentBrain(enemy, SecondBrain);
        }

        base.Execute(enemy);
    }
}
