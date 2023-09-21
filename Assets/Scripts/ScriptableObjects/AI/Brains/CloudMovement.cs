using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Cloud Movement")]
public class CloudMovement : Brain
{
    public override void Enter()
    {
        
    }

    public override void Exit()
    {

    }

    public override void Think(Enemy enemy)
    {
        if (enemy.GetEnemyPath() != null)
        {
            float time = enemy.GetTimePosition();
            time += Time.deltaTime * Speed.Value;

            if (time < 0) { enemy.DisableObject(); return; }

            // If the enemy reaches the end of the path, means it reached the pie
            if (time >= enemy.GetEnemyPath().path.length)
            {
                enemy.TriggerOnReachingPie();
                return;
            }

            enemy.transform.position = enemy.GetEnemyPath().path.GetPointAtDistance(time);

            float newScale = enemy.GetInicialScale() + (1f - enemy.GetInicialScale()) * (time / enemy.GetEnemyPath().path.length);
            enemy.transform.localScale = new Vector3(newScale, newScale, 0);

            enemy.SetOldElapsedTime(time);
        }
    }
}
