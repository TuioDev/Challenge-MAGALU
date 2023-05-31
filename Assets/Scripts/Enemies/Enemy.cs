using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    //This can be a list, so that we add different behaviours and change them during the game
    [SerializeField] private Brain AIBehaviour;
    [SerializeField] private int MaxHealth;

    private Health EnemyHealth = new();
    private PathCreator EnemyPath;

    // Start is called before the first frame update
    void Start()
    {
        EnemyHealth.SetAmount(MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        AIBehaviour.Think(this);
    }

    public void TakeDamageOrHeal(int damage)
    {
        EnemyHealth.TakeDamage(damage);
    }

    public void SetPath(PathCreator path)
    {
        EnemyPath = path;
    }
    //Here we put the functions to change behaviour

}
