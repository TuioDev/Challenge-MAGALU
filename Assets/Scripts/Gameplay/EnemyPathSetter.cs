using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathSetter : MonoBehaviour
{
    [SerializeField] private PathCollection WalkablePaths;
    [SerializeField] private PathCollection SpiderPaths;
    [SerializeField] private List<PathCreator> AllEnemyPaths;

    void Awake()
    {
        SetPathCollection(WalkablePaths);
    }

    private void SetPathCollection(PathCollection pathCollection)
    {
        if (pathCollection != null)
        {
            //Everytime we open the level we need to set the paths to the ScriptableObject
            //so we clear the object before setting, because it can lead to null reference
            pathCollection.GetPathCreators().Clear();

            foreach (PathCreator enemyPath in AllEnemyPaths)
            {
                //Debug.Log("Path " + enemyPath.gameObject.name + ": " + enemyPath.path.length);
                if (!pathCollection.GetPathCreators().Contains(enemyPath))
                {
                    pathCollection.GetPathCreators().Add(enemyPath);
                }
            }
        }
    }
}
