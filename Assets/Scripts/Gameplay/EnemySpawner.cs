using PathCreation;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn settings")]
    [SerializeField] private float InitialDelay;
    [SerializeField] private float InitialDelayBetweenEnemies;
    [SerializeField] private float InitialDelayBetweenWaves;
    [Header("Weight settings")]
    [SerializeField] private int InitialWeightPoints;
    [SerializeField] private int FinalWeightPoints;
    [SerializeField] private MathematicalCalculation WeightPointCalculation;
    [field: Header("References")]
    [field: SerializeField] public FloatVariable TotalRoundTime { get; private set; }
    [field: SerializeField] public FloatVariable CurrentLevelTime { get; private set; }
    [field: Header("Curve Settings")]
    [field: SerializeField] public AnimationCurve LinearCalculationCurve { get; private set; }
    [field: SerializeField] public AnimationCurve ExponentialCalculationCurve { get; private set; }

    private Transform PieTransform;
    private EnemyPathSetter PathSetter;
    private List<EnemyReference> EnemiesThisLevel = new();
    private Queue<Enemy> WaveOfEnemies = new();
    private Enemy LastEnemyBought;
    private int CurrentWeightPoints;
    private float CurrentDelayBetweenEnemies;
    private float CurrentDelayBetweenWaves;

    private Keyframe[] CurveFrames = new Keyframe[2];

    // Start is called before the first frame update
    void Awake()
    {
        PathSetter = FindObjectOfType<EnemyPathSetter>();
        PieTransform = FindObjectOfType<Pie>().transform;
        SetInitialValues();
        SetCurvesInitialValues();
        SetEnemiesThisLevel();
    }

    void Start()
    {
        StartCoroutine(LevelStart());
    }

    private void SetInitialValues()
    {
        CurrentDelayBetweenEnemies = InitialDelayBetweenEnemies;
        CurrentDelayBetweenWaves = InitialDelayBetweenWaves;
        CurrentWeightPoints = InitialWeightPoints;
    }

    private void SetCurvesInitialValues()
    {
        // Set Linear curve initial value
        CurveFrames = LinearCalculationCurve.keys;
        CurveFrames[0].value = InitialWeightPoints;
        CurveFrames[0].time = 0f;
        CurveFrames[1].value = FinalWeightPoints;
        CurveFrames[1].time = TotalRoundTime.Value;

        LinearCalculationCurve.keys = CurveFrames;

        // Set Exponential curve initial value
        CurveFrames = ExponentialCalculationCurve.keys;
        CurveFrames[0].value = InitialWeightPoints;
        CurveFrames[0].time = 0f;
        CurveFrames[1].value = FinalWeightPoints;
        CurveFrames[1].time = TotalRoundTime.Value;

        ExponentialCalculationCurve.keys = CurveFrames;

        CurveFrames = null;
    }

    private void SetEnemiesThisLevel()
    {
        EnemiesThisLevel.Clear();
        foreach (PoolingObject pooledObject in ObjectPool.Instance.GetObjectsToPool())
        {
            if (pooledObject.Prefab.GetComponent<Enemy>() == null) { continue; }

            EnemyReference newEnemyReference = new()
            {
                EnemyComponent = pooledObject.Prefab.GetComponent<Enemy>(),
                WeightPoints = pooledObject.WeightPoints
            };

            EnemiesThisLevel.Add(newEnemyReference);
        }
    }

    private IEnumerator LevelStart()
    {
        yield return new WaitForSeconds(InitialDelay);
        StartCoroutine(SpawnEnemies());
    }
    private IEnumerator SpawnEnemies()
    {
        WaveOfEnemies.Clear();

        // Get a list of enemies from the weight system
        while (CurrentWeightPoints > 0)
        {
            ShuffleList<EnemyReference>(EnemiesThisLevel);

            // Ensure that between different enemies there is always an ant
            if (LastEnemyBought != null && LastEnemyBought is not Ant)
            {
                WaveOfEnemies.Enqueue(EnemiesThisLevel.Find(x => x.EnemyComponent is Ant).EnemyComponent);
            }

            for (int i = 0; ; i++)
            {
                if (EnemiesThisLevel[i].WeightPoints <= CurrentWeightPoints)
                {
                    CurrentWeightPoints -= EnemiesThisLevel[i].WeightPoints;
                    WaveOfEnemies.Enqueue(EnemiesThisLevel[i].EnemyComponent);
                    LastEnemyBought = EnemiesThisLevel[i].EnemyComponent;
                    break;
                }
            }
        }

        // Start spawning the enemy wave
        StartCoroutine(SpawnWave(WaveOfEnemies));

        yield return null;
    }
    
    private IEnumerator SpawnWave(Queue<Enemy> enemyWave)
    {
        // Spawn current wave
        while (enemyWave.Count > 0)
        {
            Enemy enemy = enemyWave.Dequeue();

            if (enemy is Ant)
            {
                SetEnemy(ObjectPool.Instance.GetInactivePooledObject<Ant>());
            }
            else if (enemy is Cloud)
            {
                SetEnemyCloud(ObjectPool.Instance.GetInactivePooledObject<Cloud>());
            }
            else
            {
                SetEnemySpider(ObjectPool.Instance.GetInactivePooledObject<Spider>());
            }

            yield return new WaitForSeconds(CurrentDelayBetweenEnemies);
        }

        // Update the amount of points for the next wave
        CalculateNewWeightPoint();

        yield return new WaitForSeconds(CurrentDelayBetweenWaves);
        StartCoroutine(SpawnEnemies());
    }

    #region Enemies Setters
    private void SetEnemy(Enemy enemy)
    {
        // Gives the path reference to the enemy and sets the position
        enemy.SetEnemyPath(PathSetter.GetWalkablePathCollection().GetRandomPath());
        enemy.transform.position = enemy.GetEnemyPath().path.GetPointAtTime(0);
        enemy.ResetEnemy();
        enemy.gameObject.SetActive(true);
    }

    private void SetEnemySpider(Spider spider)
    {
        // Get a random path for the spider
        PathCreator spiderPath = PathSetter.GetSpiderPathCollection().GetRandomPath();
        if (spiderPath == null) return;

        // Check if the path is already active
        if (!spiderPath.isActiveAndEnabled)
        {
            spiderPath.gameObject.SetActive(true);

            // Sets the initial and final position for the path
            Vector3 initialPosition = GetRandomPointOutsideOfView();
            initialPosition.z = 0f;

            Vector3 direction = new(
                initialPosition.x - PieTransform.position.x,
                initialPosition.y - PieTransform.position.y,
                0.0f
                );
            // The 0.4 is based on the pie radius so that the path's end isn't on top of the sprite
            Vector3 finalPosition = new(
                PieTransform.position.x + 0.4f * (direction.normalized.x),
                PieTransform.position.y + 0.4f * (direction.normalized.y),
                0.0f
                );

            // Modify its anchor points
            spiderPath.bezierPath.MovePoint(0, initialPosition);
            spiderPath.bezierPath.MovePoint(3, finalPosition);

            // Send the angle to the Path's ReferenceKeeper so that it can set the sprite position
            float angleBetweenPositions = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            PathSetter.AllSpiderPathAndReferences[spiderPath].
                SetSpriteTransform(initialPosition, PieTransform.position, angleBetweenPositions);

            // Get the middle position
            Vector3 middlePosition = new Vector3(
                    (finalPosition.x + initialPosition.x) / 2,
                    (finalPosition.y + initialPosition.y) / 2,
                    0.0f
                    );

            // Modify the other points so that the path remains in a line
            spiderPath.bezierPath.MovePoint(1, middlePosition);
            spiderPath.bezierPath.MovePoint(2, middlePosition);

            spiderPath.editorData.VertexPathSettingsChanged();
        }

        // Pass the enemy as a reference to the path
        PathSetter.AllSpiderPathAndReferences[spiderPath].AddEnemyOnPath(spider);
        
        // Gives the path reference to the spider and sets the position
        spider.SetEnemyPath(spiderPath);

        spider.transform.position = spider.GetEnemyPath().path.GetPointAtTime(0);
        spider.ResetEnemy();

        // Wait for the timer
        StartCoroutine(spider.WaitWebTime());
    }

    private void SetEnemyCloud(Cloud cloud)
    {
        // Gives the path reference to the enemy and sets the position
        cloud.SetEnemyPath(PathSetter.GetCloudPathCollection().GetRandomPath());
        cloud.transform.position = cloud.GetEnemyPath().path.GetPointAtTime(0);
        cloud.ResetEnemy();
        cloud.enabled = true;
        cloud.gameObject.SetActive(true);
    }

    private Vector3 GetRandomPointOutsideOfView()
    {
        float x = Random.Range(-0.1f, 0.1f);
        float y = Random.Range(-0.1f, 0.1f);

        if (x >= 0) x += 1;
        if (y >= 0) y += 1;

        Vector3 pointOutsideOfView = new(x, y);

        return Camera.main.ViewportToWorldPoint(pointOutsideOfView);
    }
    #endregion

    #region LevelProgressionCalculation
    private void CalculateNewWeightPoint()
    {
        switch (WeightPointCalculation)
        {
            case MathematicalCalculation.Constant:
                SetValueAsConstant();
                break;
            case MathematicalCalculation.Linear:
                SetValueAsLinear();
                break;
            case MathematicalCalculation.Exponential:
                SetValueAsExponential();
                break;
            default:
                break;
        }
    }

    private void SetValueAsConstant()
    {
        CurrentWeightPoints = InitialWeightPoints;
        //CurrentDelayBetweenEnemies = InitialDelayBetweenEnemies;
        //CurrentDelayBetweenWaves = InitialDelayBetweenWaves;
    }

    private void SetValueAsLinear()
    {
        CurrentWeightPoints = (int)LinearCalculationCurve.Evaluate(CurrentLevelTime.Value);
        //CurrentDelayBetweenEnemies = InitialDelayBetweenEnemies;
        //CurrentDelayBetweenWaves = InitialDelayBetweenWaves;
    }

    private void SetValueAsExponential()
    {
        CurrentWeightPoints = (int)ExponentialCalculationCurve.Evaluate(CurrentLevelTime.Value);
    }
    #endregion

    private List<T> ShuffleList<T>(List<T> list)
    {
        int index = list.Count;
        while (index > 1)
        {
            index--;
            int randomIndex = Random.Range(0, index + 1);
            T item = list[index];
            list[index] = list[randomIndex];
            list[randomIndex] = item;
        }

        return list;
    }
}

[System.Serializable]
public class EnemyReference
{
    public Enemy EnemyComponent;
    public int WeightPoints;
}

public enum MathematicalCalculation
{
    Constant,
    Linear,
    Exponential
}