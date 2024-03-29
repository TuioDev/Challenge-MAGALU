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
    [SerializeField] private protected FloatVariable MaxHealth;
    [SerializeField][Range(.1f, 1f)] private float InicialScale;

    private protected float TimePosition = 0f; // The position on the path is based on time
    private protected float Speed; // If something needs to change the speed of each enemy
    private protected Animator EnemyAnimator;
    private protected Brain CurrentBrain;
    private protected PathCreator EnemyPath;
    private protected Health EnemyHealth = new();
    private protected bool IsInUpdateRoutine = false;

    //public void SetCurrentBehaviour(AIBehaviour aIBehaviour) => CurrentBehaviour = aIBehaviour; // We can apply a new Behaviour
    public float GetSpeed() => Speed;
    public float GetInicialScale() => InicialScale;
    public void SetEnemyPath(PathCreator path) => EnemyPath = path;
    public PathCreator GetEnemyPath() => EnemyPath;
    public void SetCurrentBrain(Brain brain) => CurrentBrain = brain;
    public Brain GetCurrentBrain() => CurrentBrain;
    public float GetTimePosition() => TimePosition;
    public void SetOldElapsedTime(float newElapsedTime) => TimePosition = newElapsedTime;

    // The Awake is used here so that the object is not Active when Instantiated and don't call OnEnable
    private void Awake()
    {
        EnemyAnimator = GetComponent<Animator>();
        EnemyAnimator.keepAnimatorStateOnDisable = true;
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
        EnemyHealth.SetAmount(MaxHealth.Value);
        this.transform.localScale = new Vector3(GetInicialScale(), GetInicialScale(), 1);
    }

    public virtual void DisableObject()
    {
        this.gameObject.SetActive(false);
    }

    public virtual void TriggerOnReachingPie()
    {
        OnReachingPieEvent.TriggerEvent();
    }
}
