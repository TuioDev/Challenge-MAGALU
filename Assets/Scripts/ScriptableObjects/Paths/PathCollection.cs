using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[CreateAssetMenu(menuName = "Path Collection")]
public class PathCollection : ScriptableObject
{
    private List<PathCreator> PathCreators = new();
    private List<PathCreator> ActivePaths = new();

    public List<PathCreator> GetPathCreators() => PathCreators;
    public List<PathCreator> GetActivePaths() => ActivePaths;

    public PathCreator GetRandomPath() => PathCreators[Random.Range(0, PathCreators.Count)];

    public void AddActivePath(PathCreator path) => ActivePaths.Add(path);
    public void RemoveActivePath(PathCreator path) => ActivePaths.Remove(path);
}
