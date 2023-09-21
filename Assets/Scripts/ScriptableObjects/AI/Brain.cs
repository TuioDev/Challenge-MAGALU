using UnityEngine;

// This is a State Pattern
public abstract class Brain : ScriptableObject
{
    [SerializeField] private protected FloatVariable Speed;
    public abstract void Enter();
    public abstract void Think(Enemy enemy);
    public abstract void Exit();
}
