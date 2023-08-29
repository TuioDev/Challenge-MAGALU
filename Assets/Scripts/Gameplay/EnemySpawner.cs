using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn settings")]
    [SerializeField] private float InitialDelay;
    //This probably will be changed, but for now it works to test everything
    [SerializeField] private float DelayBetweenEnemies;
    [Header("References")]
    [SerializeField] private FloatVariable LevelTime;

    private Transform PieTransform;
    private EnemyPathSetter PathSetter;

    // See if we can get this, but every enemy will have to despawn, or when it dies call an event
    //private List<Enemy> AllEnemiesActive = new List<Enemy>();

    /// <summary>
    /// TODO: BETTER SPAWNER BASED ON GAME TIME
    /// Get a list of some enemies:
    ///     The later the phase goes, the higher the amount
    /// Get the paths based on each ones hidden values to modify the probabilites
    /// </summary>

    // Start is called before the first frame update
    void Awake()
    {
        PathSetter = FindObjectOfType<EnemyPathSetter>();
        PieTransform = FindObjectOfType<Pie>().transform;
    }

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemies), InitialDelay, DelayBetweenEnemies); // CHANGE THIS LINE
    }

    private void SpawnEnemies()
    {
        // TODO: If we want a more organic level we change this line
        Enemy enemy = ObjectPool.Instance.GetRandomInactiveEnemy();

        if (enemy != null)
        {
            if (enemy is Spider) { SetEnemySpider(enemy); return; }
            if (enemy is Cloud) SetEnemyCloud(enemy as Cloud);

            SetEnemy(enemy);
        }
    }

    private void SetEnemy(Enemy enemy)
    {
        // Gives the path reference to the enemy and sets the position
        enemy.SetEnemyPath(PathSetter.GetWalkablePathCollection().GetRandomPath());
        enemy.transform.position = enemy.GetEnemyPath().path.GetPointAtTime(0);
        enemy.ResetEnemy();
        enemy.gameObject.SetActive(true);
    }

    private void SetEnemySpider(Enemy enemy)
    {
        // Get a random position around the pie
        // Get position of the pie
        // Set point 0 and 3 as this positions
        // Set points 1 and 2 as something in between

        // Get a random path for the spider
        PathCreator spiderPath = PathSetter.GetSpiderPathCollection().GetRandomPath();
        if (spiderPath == null) return;
        PathSetter.AllSpiderPathAndReferences[spiderPath].AddEnemyOnPath(enemy);

        spiderPath.gameObject.SetActive(true);

        // Sets the initial and final position for the path
        Vector3 initialPosition = GetRandomPointOutsideOfView();
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

        // Get the middle position
        Vector3 middlePosition = new Vector3(
                (finalPosition.x + initialPosition.x) / 2,
                (finalPosition.y + initialPosition.y) / 2,
                0.0f
                );

        // Modify the other points so that the path remains in a line
        spiderPath.bezierPath.MovePoint(1, middlePosition);
        spiderPath.bezierPath.MovePoint(2, middlePosition);

        // Gives the path reference to the spider and sets the position
        enemy.SetEnemyPath(spiderPath);
        
        enemy.transform.position = enemy.GetEnemyPath().path.GetPointAtTime(0);
        enemy.ResetEnemy();
        enemy.gameObject.SetActive(true);
    }

    private void SetEnemyCloud(Enemy enemy)
    {

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
}
