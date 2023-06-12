using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Simple Movement")]
public class SimpleMovement : Brain
{
    [SerializeField] private float Speed;

    //This is shared by all of the enemies

    public override void Think(Enemy enemy, float time)
    {
        if (enemy.EnemyPath != null)
        {
            time += (Time.deltaTime * Speed);

            // If the enemy reaches the end of the path, it means if reached the pie
            if (time >= enemy.EnemyPath.path.length)
            {
                enemy.DisableObject();
                enemy.TriggerOnReachingPie();
                return;
            }

            enemy.transform.position = enemy.EnemyPath.path.GetPointAtDistance(time);
            enemy.transform.rotation = enemy.EnemyPath.path.GetRotationAtDistance(time) * Quaternion.Euler(0, -90, -90);
            enemy.SetOldElapsedTime(time);
        }
    }
}
