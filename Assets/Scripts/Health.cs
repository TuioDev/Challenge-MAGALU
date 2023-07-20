using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    public float CurrentAmount { get; private set; }

    public void SetAmount(float value)
    {
        CurrentAmount = value;
    }
    public void TakeDamage(float value)
    {
        CurrentAmount -= value;
    }

    public void Recover(float value)
    {
        CurrentAmount += value;
    }
}
