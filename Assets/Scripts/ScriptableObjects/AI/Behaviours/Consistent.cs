using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviour/Consistent", fileName = "Consistent")]
public class Consistent : AIBehaviour
{
    [Header("Brain")]
    [SerializeField] private Brain OnlyBrain;

    public override void Initialize(Enemy enemy)
    {
        enemy.SetCurrentBrain(OnlyBrain);
    }

    public override void Execute(Enemy enemy)
    {
        enemy.GetCurrentBrain().Think(enemy);
    }
}
