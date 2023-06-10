using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Simple Movement")]
public class SimpleMovement : Brain
{
    [SerializeField] private float Speed;

    //This is shared by all of the enemies
    //TODO: IMPLEMENT THIS IN ENEMY CLASS

    public override void Think(Enemy enemy, float time)
    {
        if (enemy.EnemyPath != null)
        {
            time += (Time.deltaTime * Speed);
            if (time >= enemy.EnemyPath.path.length) enemy.DisableObject(); // An ant was able to walk to the end
            enemy.transform.position = enemy.EnemyPath.path.GetPointAtDistance(time);
            enemy.transform.rotation = enemy.EnemyPath.path.GetRotationAtDistance(time) * Quaternion.Euler(0, -90, -90);
            enemy.SetOldElapsedTime(time);
        }
    }
}
