using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathSetter : MonoBehaviour
{
    [Header("Path collections")]
    [SerializeField] private PathCollection WalkablePathCollection;
    [SerializeField] private PathCollection SpiderPathCollection;

    [Header("List of enemy paths for each collection")]
    [SerializeField] private List<PathCreator> AllWalkablePaths;
    [SerializeField] private List<PathCreator> AllSpiderPaths;

    public PathCollection GetWalkablePathCollection() => WalkablePathCollection;
    public PathCollection GetSpiderPathCollection() => SpiderPathCollection;

    void Awake()
    {
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
}
