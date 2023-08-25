using PathCreation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy base info")]
    [SerializeField] private protected AIBehaviour CurrentBehaviour;
    [SerializeField] private protected GameEvent OnReachingPieEvent;

    [Header("Enemy stats")]
    [SerializeField] private protected int MaxHealth;

    private protected Brain CurrentBrain;
    // The position on the path is based on time
    private protected float TimePosition = 0f;
    private protected PathCreator EnemyPath;
    private protected Health EnemyHealth = new();

    //public void SetCurrentBehaviour(AIBehaviour aIBehaviour) => CurrentBehaviour = aIBehaviour;
    public void SetEnemyPath(PathCreator path) => EnemyPath = path;
    public PathCreator GetEnemyPath() => EnemyPath;
    public void SetCurrentBrain(Brain brain) => CurrentBrain = brain;
    public Brain GetCurrentBrain() => CurrentBrain;
    public float GetTimePosition() => TimePosition;
    public void SetOldElapsedTime(float newElapsedTime) => TimePosition = newElapsedTime;

    // The Awake is used here so that the object is not Active when Instantiated and don't call OnEnable
    private void Awake()
    {
        DisableObject();
    }

    private protected void Update()
    {
        // Execute the AIBehaviour, make the behaviour act as it has an Update
        ExecuteEnemyBehaviour(this);
    }

    private protected virtual void ExecuteEnemyBehaviour(Enemy enemy)
    {
        CurrentBehaviour.Execute(enemy);
    }

    public void SetPath(PathCreator path)
    {
        EnemyPath = path;
    }

    public virtual void ResetEnemy()
    {
        CurrentBehaviour.Initialize(this);
        TimePosition = 0f;
        EnemyHealth.SetAmount(MaxHealth);
    }

    public void DisableObject()
    {
        this.gameObject.SetActive(false);
    }

    public virtual void TriggerOnReachingPie()
    {
        OnReachingPieEvent.TriggerEvent();
    }
}
