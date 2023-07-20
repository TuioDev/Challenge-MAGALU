using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviour/Change in-between with event", fileName = "ChangeInbetweenWithEvent")]
public class ChangeInbetweenWithEvent : AIBehaviour
{
    // This is called with the Game Event Listener with OnWindPush
    public void ChangeBackAndForth(Enemy enemy)
    {
        if(enemy.GetCurrentBrain() == InitialBrain)
        {
            enemy.SetCurrentBrain(SecondBrain);
        }
        else
        {
            enemy.SetCurrentBrain(InitialBrain);
        }
    }

    // This is called with the Game Event Listener with OnWindStop
    public void ChangeToInitialBrain(Enemy enemy)
    {
        enemy.SetCurrentBrain(InitialBrain);
    }
}
