using UnityEngine;


// All the other information must be created in a class that inherited this
public abstract class AIBehaviour : ScriptableObject
{
    [field: Header("Event for the behaviour")]
    [field: SerializeField] public GameEvent DesignatedEvent { get; private protected set; }

    [field: Header("Brains")]
    [field: SerializeField] public Brain InitialBrain { get; private protected set; }
    [field: SerializeField] public Brain SecondBrain { get; private protected set; }

    // This works like a Start function
    public virtual void Initialize(Enemy enemy) { enemy.SetCurrentBrain(InitialBrain); }
    // This works like an Update function, every class implementing this needs the proper logic in this method
    public virtual void Execute(Enemy enemy) { enemy.GetCurrentBrain().Think(enemy); }

    public void ChangeCurrentBrain(Enemy enemy, Brain newBrain)
    {
        if(enemy.GetCurrentBrain() == newBrain) return;

        ChangeBrain(enemy, newBrain);
    }

    private void ChangeBrain(Enemy enemy, Brain newBrain)
    {
        enemy.GetCurrentBrain().Exit();

        enemy.SetCurrentBrain(newBrain);

        enemy.GetCurrentBrain().Enter();
    }
}
