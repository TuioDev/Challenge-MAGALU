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
        if (enemy.EnemyPath != null) enemy.transform.position = enemy.EnemyPath.path.GetPointAtDistance(time * Speed);
    }
}
