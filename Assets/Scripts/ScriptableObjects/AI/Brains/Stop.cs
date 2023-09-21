using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Stop")]
public class Stop : Brain
{
    public override void Think(Enemy enemy)
    {
        // Update
    }

    public override void Enter()
    {
        // When changing into this brain
    }

    public override void Exit()
    {
        // When leaving this brain
    }
}
