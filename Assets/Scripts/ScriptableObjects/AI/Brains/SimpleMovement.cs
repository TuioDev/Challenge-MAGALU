using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Simple Movement")]
public class SimpleMovement : Brain
{
    [Header("Movement stats")]
    [SerializeField] private float Speed;

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
            time += Time.deltaTime * Speed;

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

            //float test = 1f + time / enemy.GetEnemyPath().path.length;
            //enemy.transform.localScale = new Vector3(test, test, enemy.transform.localScale.z);
            enemy.SetOldElapsedTime(time);
        }
    }
}
