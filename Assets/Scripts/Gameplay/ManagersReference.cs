using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagersReference : MonoBehaviour
{
    [SerializeField] private GameObject EnemySpawnerReference;
    [SerializeField] private GameObject TimeManagerReference;

    public GameObject GetEnemySpawner() => EnemySpawnerReference;
    public GameObject GetTimeManager() => TimeManagerReference;
}
