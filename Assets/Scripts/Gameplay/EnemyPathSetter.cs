using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathSetter : MonoBehaviour
{
    [SerializeField] private PathCollection WalkablePaths;
    [SerializeField] private List<PathCreator> AllEnemyPaths;

    void Awake()
    {
        SetPathCollection();
    }

    private void SetPathCollection()
    {
        if (WalkablePaths != null)
        {
            //Everytime we open the level we need to set the paths to the ScriptableObject
            //so we clear the object before setting, because it can lead to null reference
            WalkablePaths.GetPathCreators().Clear();

            foreach (PathCreator enemyPath in AllEnemyPaths)
            {
                if (!WalkablePaths.GetPathCreators().Contains(enemyPath))
                {
                    WalkablePaths.GetPathCreators().Add(enemyPath);
                }
            }
        }
    }
}
