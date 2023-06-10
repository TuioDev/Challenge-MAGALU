using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[CreateAssetMenu(menuName = "Path Collection")]
public class PathCollection : ScriptableObject
{
    private List<PathCreator> PathCreators = new List<PathCreator>();

    public List<PathCreator> GetPathCreators()
    {
        return PathCreators;
    }

    public PathCreator GetRandomPath()
    {
        return PathCreators[Random.Range(0, PathCreators.Count)];
    }
}
