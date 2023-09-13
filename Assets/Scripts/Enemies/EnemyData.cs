using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Create Enemy Data")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private GameObject Prefab;
    [SerializeField] private int CostToSpawn;

    public GameObject GetPrefab() => Prefab;
    public int GetCostToSpawn() => CostToSpawn;

    public Enemy GetEnemyComponent()
    {
        return Prefab.GetComponent<Enemy>();
    }
}
