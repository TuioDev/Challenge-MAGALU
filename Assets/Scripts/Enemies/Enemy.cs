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

    private protected Brain CurrentBrain;
    // The position on the path is based on time
    private protected float TimePosition = 0f;
    private PathCreator EnemyPath;

    //public void SetCurrentBehaviour(AIBehaviour aIBehaviour) => CurrentBehaviour = aIBehaviour;
    public PathCreator GetEnemyPath() => EnemyPath;
    public void SetCurrentBrain(Brain brain) => CurrentBrain = brain;
    public Brain GetCurrentBrain() => CurrentBrain;
    public void SetEnemyPath(PathCreator path) => EnemyPath = path;
    public float GetTimePosition() => TimePosition;
    public void SetOldElapsedTime(float newElapsedTime) => TimePosition = newElapsedTime;

    // The Awake is used here so that the object is not Active when Instantiated and call OnEnable
    private void Awake()
    {
        DisableObject();
    }

    private protected void Update()
    {
        // Execute the AIBehaviour, make the behaviour act as it has an Update
        ExecuteEnemyBehaviour(this);
    }

    private void ExecuteEnemyBehaviour(Enemy enemy)
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
    }

    public void DisableObject()
    {
        this.gameObject.SetActive(false);
    }

    public void TriggerOnReachingPie()
    {
        OnReachingPieEvent.TriggerEvent();
    }

}
