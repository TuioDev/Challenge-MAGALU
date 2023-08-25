using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesReferenceKeeper : MonoBehaviour
{
    private List<Enemy> EnemiesOnPath = new();
    public List<Enemy> GetEnemiesOnPath() => EnemiesOnPath;
    public void AddEnemyOnPath(Enemy enemy) => EnemiesOnPath.Add(enemy);
    public void RemoveEnemyOnPath(Enemy enemy)
    {
        EnemiesOnPath.Remove(enemy);
        if(EnemiesOnPath.Count == 0) this.gameObject.SetActive(false);
    }
}
