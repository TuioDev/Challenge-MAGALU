using PathCreation;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPathSetter : MonoBehaviour
{
    [Header("Path collections")]
    [SerializeField] private PathCollection WalkablePathCollection;
    [SerializeField] private PathCollection SpiderPathCollection;

    [Header("List of enemy paths for each collection")]
    [SerializeField] private List<PathCreator> AllWalkablePaths;
    [SerializeField] private List<PathCreator> AllSpiderPaths;

    [Header("Spider path prefab and amount")]
    [SerializeField] private GameObject SpiderPathPrefab;
    [SerializeField] private float AmountOfSpiderPaths;

    public PathCollection GetWalkablePathCollection() => WalkablePathCollection;
    public PathCollection GetSpiderPathCollection() => SpiderPathCollection;

    public Dictionary<PathCreator, EnemiesReferenceKeeper> AllSpiderPathAndReferences = new();

    void Awake()
    {
        SpawnSpiderPaths();

        SetPathCollection(WalkablePathCollection);
        SetPathCollection(SpiderPathCollection);
    }

    private void SetPathCollection(PathCollection pathCollection)
    {
        if (pathCollection != null)
        {
            //Everytime we open the level we need to set the paths to the ScriptableObject
            //so we clear the object before setting, because it can lead to null reference
            pathCollection.GetPathCreators().Clear();
            pathCollection.GetActivePaths().Clear();

            foreach (var enemypath in GetListForTheCollection(pathCollection.name.ToString()))
            {
                if (!pathCollection.GetPathCreators().Contains(enemypath))
                {
                    pathCollection.GetPathCreators().Add(enemypath);
                }
            }
        }
    }

    // Every new path needs to be added here, the string must be equal to the SO name
    private List<PathCreator> GetListForTheCollection(string collectionName)
    {
        return collectionName switch
        {
            "WalkablePathCollection" => AllWalkablePaths,
            "SpiderPathCollection" => AllSpiderPaths,
            _ => null,
        };
    }

    private void SpawnSpiderPaths()
    {
        for (int i = 0; i < AmountOfSpiderPaths; i++)
        {
            GameObject spiderPathPrefab = Instantiate(SpiderPathPrefab);
            spiderPathPrefab.transform.SetParent(this.transform, false);

            PathCreator path = spiderPathPrefab.GetComponent<PathCreator>();
            EnemiesReferenceKeeper reference = spiderPathPrefab.GetComponent<EnemiesReferenceKeeper>();

            AllSpiderPaths.Add(path); // Keep reference just from the paths
            AllSpiderPathAndReferences.Add(path, reference); // Dictionary to get the references faster
        }
    }
}
