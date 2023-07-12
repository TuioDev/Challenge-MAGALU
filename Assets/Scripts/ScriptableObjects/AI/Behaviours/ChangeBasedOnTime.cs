using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviour/Change based on Time", fileName = "ChangeBasedOnTime")]
public class ChangeBasedOnTime : AIBehaviour
{
    [Header("Brains")]
    [SerializeField] private Brain NormalMovement;
    [SerializeField] private Brain FastMovement;

    [Header("Change Based on Time")]
    [SerializeField][Range(1, 30)] private int TimeInSecondsToTrigger;
    [SerializeField] private GameEvent OnPassingTime;

    public override void Initialize(Enemy enemy)
    {
        enemy.SetCurrentBrain(NormalMovement);
    }

    public override void Execute(Enemy enemy)
    {
        if (enemy.GetTimePosition() >= TimeInSecondsToTrigger)
        {
            OnPassingTime.RemoveListener(enemy.GetComponent<GameEventListener>());
            ChangeCurrentBrain(enemy, FastMovement);
        }
    }

}
