using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// The functions are called in the Game Event Listener
[RequireComponent(typeof(GameEventListener))]
public class Spider : Enemy, IDamageable, IPushable
{
    [Header("Spider info")]
    [SerializeField] private float PushSpeed;

    public int GetEnemyHealthPercentage() => 0;

    public void Pushed()
    {
        // Change the Brain?
        
    }

    public void Released()
    {
        // Change the Brain back to normal?
    }

    public void TakeDamageOrHeal(int damage)
    {
        
    }
}
