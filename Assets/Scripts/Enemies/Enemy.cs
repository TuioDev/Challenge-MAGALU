using PathCreation;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private List<Brain> AIBehaviour;
    [SerializeField] private int MaxHealth;
    public PathCreator EnemyPath; //This will be private, just checking if it works

    private Health EnemyHealth = new();

    // The position on the path is based on time
    private float PathTimePosition = 0f;


    // Start is called before the first frame update
    void Start()
    {
        DisableObject();
    }

    // Update is called once per frame
    void Update()
    {
        Behaviour();
    }

    private void OnEnable()
    {
        ResetEnemy();
    }
    private void Behaviour()
    {
        //ExecuteBehaviour(1);
        if (EnemyHealth.Amount > MaxHealth / 2)
        {
            ExecuteBehaviour(0);
        }
        else
        {
            ExecuteBehaviour(1);
        }
    }

    private void ExecuteBehaviour(int index)
    {
        AIBehaviour[index]?.Think(this, PathTimePosition);
    }

    public void TakeDamageOrHeal(int damage)
    {
        EnemyHealth.TakeDamage(damage);
        if (EnemyHealth.Amount <= 0)
        {
            DisableObject();
        }
    }

    public void SetPath(PathCreator path)
    {
        EnemyPath = path;
    }

    private void ResetEnemy()
    {
        EnemyHealth.SetAmount(MaxHealth);
        PathTimePosition = 0f;
    }

    public void SetOldElapsedTime(float newElapsedTime)
    {
        PathTimePosition = newElapsedTime;
    }
    public void DisableObject()
    {
        this.gameObject.SetActive(false);
    }
    //Here we put the functions to change behaviour

}
