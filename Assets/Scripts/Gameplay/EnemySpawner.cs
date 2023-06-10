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
    private bool IsRunning = false;
    private float ElapsedTime = 0f;

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
        IsRunning = true; //This should be in a method
        //StartCoroutine(SpawnEnemimes());

        Invoke("SpawnOneEnemy", 2f);
    }

    private void SetEnemiesReferences()
    {
        foreach(EnemyData data in AllEnemiesData)
        {
            if (data != null)
            {
                AllEnemiesReference.Add(data.GetEnemyComponent());
            }
        }
    }

    IEnumerator SpawnEnemimes()
    {
        ElapsedTime += Time.deltaTime;
        Debug.Log("Elapsed time: " + ElapsedTime);

        if (ElapsedTime >= InitialDelay)
        {
            while (IsRunning)
            {
                GameObject tmp = AllEnemiesData[Random.Range(0, AllEnemiesData.Length)].GetPrefab();
                //tmp.GetComponent<Enemy>().SetPath();
                yield return new WaitForSeconds(DelayBetweenEnemies);
            }
        }
    }

    public void SpawnOneEnemy()
    {
        Enemy enemy = AllEnemiesReference[Random.Range(0, AllEnemiesReference.Count)];
        enemy.EnemyPath = WalkablePaths.GetRandomPath();
        Instantiate(enemy);
        //enemy.SetPath(Paths.GetRandomPath());
        //enemy.transform.position = enemy.EnemyPath.path.GetPointAtDistance(0);
    }
}
