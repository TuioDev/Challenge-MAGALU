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
    private float ElapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        EnemyHealth.SetAmount(MaxHealth);
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Behaviour();
        TimeTick();
    }

    private void Behaviour()
    {
        ExecuteBehaviour(1);
        //if (EnemyHealth.Amount > MaxHealth/2)
        //{
        //    ExecuteBehaviour(0);
        //}
        //else
        //{
        //    ExecuteBehaviour(1);
        //}
    }

    private void ExecuteBehaviour(int index)
    {
        AIBehaviour[index].Think(this, ElapsedTime);
    }

    private void TimeTick()
    {
        ElapsedTime += Time.deltaTime;
    }

    public void TakeDamageOrHeal(int damage)
    {
        EnemyHealth.TakeDamage(damage);
        if (EnemyHealth.Amount <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void SetPath(PathCreator path)
    {
        EnemyPath = path;
    }
    //Here we put the functions to change behaviour

}
