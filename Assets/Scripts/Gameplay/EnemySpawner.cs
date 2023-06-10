using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float InitialDelay;
    //This probably will be changed, but for now it works to test everything
    [SerializeField] private float DelayBetweenEnemies;
    [SerializeField] private PathCollection WalkablePaths;

    private EnemyData[] AllEnemiesData;
    private List<Enemy> AllEnemiesReference = new List<Enemy>();

    /// <summary>
    /// TODO: BETTER SPAWNER BASED ON GAME TIME
    /// Get a list of some enemies:
    ///     The later the phase goes, the higher the amount
    /// </summary>

    // Start is called before the first frame update
    void Awake()
    {
        AllEnemiesData = Resources.LoadAll<EnemyData>("Enemies");
        SetEnemiesReferences();
    }

    void Start()
    {
        InvokeRepeating("SpawnEnemies", InitialDelay, DelayBetweenEnemies);
    }

    private void SetEnemiesReferences()
    {
        foreach (EnemyData data in AllEnemiesData)
        {
            if (data != null)
            {
                AllEnemiesReference.Add(data.GetEnemyComponent());
            }
        }
    }

    private void SpawnEnemies()
    {
        Enemy enemy = ObjectPool.Instance.GetInactivePooledObject<Enemy>();
        enemy.EnemyPath = WalkablePaths.GetRandomPath();
        enemy.gameObject.SetActive(true);
    }
}
