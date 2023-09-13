//using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool _Instance;
    public static ObjectPool Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = GameObject.FindObjectOfType<ObjectPool>();
            }
            return _Instance;
        }
    }

    [Header("Objects instantiated")]
    public List<ListOfSameObject> AllLists = new();

    [SerializeField] private List<PoolingObject> ObjectsToPool;

    private List<int> EnemiesIndexList = new();

    public List<PoolingObject> GetObjectsToPool() => ObjectsToPool;

    private void Awake()
    {
        InstantiateObjects();
        SetEnemiesReferenceLists();
    }

    private void InstantiateObjects()
    {
        foreach (PoolingObject objectToPool in ObjectsToPool)
        {
            ListOfSameObject subList = new();

            for (int i = 0; i < objectToPool.AmountToSpawn; i++)
            {
                GameObject tmp = Instantiate(objectToPool.Prefab);
                subList.List.Add(tmp);
            }

            AllLists.Add(subList);
        }
    }

    public T GetInactivePooledObject<T>(int index = 0) where T : Component
    {
        if (index == 0) index = GetListIndex<T>();

        if (index < 0) return null;

        if (AllLists[index] == null) return null;

        List<GameObject> subList = AllLists[index].List;
        
        // Instead of looping through all the objects, there could be a list of inactive objects
        for (int i = 0; i < subList.Count; i++)
        {
            if (!subList[i].activeInHierarchy)
            {
                return subList[i].GetComponent<T>();
            }
        }

        // If the are no object to use, we create another one and add to the sublist
        // Find wich type of enemy needs to spawn
        
        // Get the prefab from the ObjectsToPool
        // Instantiate the prefab
        // Assing to the sublist
        GameObject newInactiveObject = Instantiate(GetPrefabReference<T>());
        AllLists[index].List.Add(newInactiveObject);

        return newInactiveObject.GetComponent<T>();
    }

    // TODO: Change this method so that it supports all lists of enemies?
    private int GetListIndex<T>() where T : Component
    {
        for (int i = 0; i < AllLists.Count; i++)
        {
            if (AllLists[i].List[0].GetComponent<T>() != null)
            {
                return i;
            }
        }
        return -1;
    }

    
    private void SetEnemiesReferenceLists()
    {
        for (int i = 0; i < ObjectsToPool.Count; i++)
        {
            if (ObjectsToPool[i].Prefab.GetComponent<Enemy>() != null)
            {
                EnemiesIndexList.Add(i);
            }
        }
    }

    public Enemy GetRandomInactiveEnemy()
    {
        return GetInactivePooledObject<Enemy>(EnemiesIndexList[Random.Range(0, EnemiesIndexList.Count)]);
    }

    // This method is returning an Ant_Normal if EnemySpawner needs a new object
    public GameObject GetPrefabReference<T>() where T : Component
    {
        for (int i = 0; i < ObjectsToPool.Count; i++)
        {
            if (ObjectsToPool[i].Prefab.GetComponent<T>() != null)
            {
                return ObjectsToPool[i].Prefab;
            }
        }

        return null;
    }
}

[System.Serializable]
public class PoolingObject
{
    public GameObject Prefab;
    public int AmountToSpawn;
    public int WeightPoints;
}

[System.Serializable]
public class ListOfSameObject
{
    public List<GameObject> List;

    public ListOfSameObject()
    {
        List = new List<GameObject>();
    }
}
