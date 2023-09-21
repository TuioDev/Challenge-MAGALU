using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "AI/Simple Movement")]
public class BaseMovement : Brain
{
    public override void Enter()
    {
        // Run Walking animation
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
                enemy.DisableObject();
                enemy.TriggerOnReachingPie();
                return;
            }

            enemy.transform.SetPositionAndRotation(
                enemy.GetEnemyPath().path.GetPointAtDistance(time),
                enemy.GetEnemyPath().path.GetRotationAtDistance(time) * Quaternion.Euler(0, -90, -90)) ;

            enemy.SetOldElapsedTime(time);
        }
    }
}
