using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Cloud Movement")]
public class CloudMovement : Brain
{
    [Header("Movement stats")]
    [SerializeField] private float Speed;

    public override void Enter()
    {
        
    }

    public override void Exit()
    {

    }

    public override void Think(Enemy enemy)
    {
        //if (IsChanneling)
        //{
        //    ExecuteChanneling();
        //    return;
        //}

        if (enemy.GetEnemyPath() != null)
        {
            float time = enemy.GetTimePosition();
            time += Time.deltaTime * Speed;

            // If the enemy reaches the end of the path, means it reached the pie
            if (time >= enemy.GetEnemyPath().path.length)
            {
                enemy.TriggerOnReachingPie();
                return;
            }

            enemy.transform.position = enemy.GetEnemyPath().path.GetPointAtDistance(time);

            float test = enemy.GetInicialScale() + (1f - enemy.GetInicialScale()) * (time / enemy.GetEnemyPath().path.length);
            enemy.transform.localScale = new Vector3(test, test, 0);
            enemy.SetOldElapsedTime(time);
        }
    }

    //private void ExecuteChanneling()
    //{
    //    if (ChannelingWasInvoked)
    //    {
    //        ChannelingWasInvoked = false;

    //        // Starts a coroutine

    //    }
    //}

    //private IEnumerator Channeling()
    //{
    //    yield return new WaitForSeconds(3f);
    //}
}
