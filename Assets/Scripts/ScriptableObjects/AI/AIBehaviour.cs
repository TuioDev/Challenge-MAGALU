using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// All the other information must be created in a class that inherited this
public abstract class AIBehaviour : ScriptableObject
{
    // This works like a Start function
    public abstract void Initialize(Enemy enemy);
    // This works like an Update function
    public abstract void Execute(Enemy enemy);

    public void ChangeCurrentBrain(Enemy enemy, Brain brain)
    {
        if(enemy.GetCurrentBrain() == brain) return;

        ChangeBrain(enemy, brain);
    }

    private void ChangeBrain(Enemy enemy, Brain brain)
    {
        enemy.GetCurrentBrain().Exit();

        enemy.SetCurrentBrain(brain);

        enemy.GetCurrentBrain().Enter();
    }
}
