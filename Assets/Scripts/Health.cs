using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    public int CurrentAmount { get; private set; }

    public void SetAmount(int value)
    {
        CurrentAmount = value;
    }
    public void TakeDamage(int value)
    {
        CurrentAmount -= value;
    }

    public void Recover(int value)
    {
        CurrentAmount += value;
    }
}
