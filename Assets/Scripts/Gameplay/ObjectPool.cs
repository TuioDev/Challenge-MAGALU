using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        InstantiateObjects();
        SetEnemiesIndexList();
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

        if (AllLists[index] != null)
        {
            List<GameObject> subList = AllLists[index].List;
            for (int i = 0; i < subList.Count; i++)
            {
                if (!subList[i].activeInHierarchy)
                {
                    return subList[i].GetComponent<T>();
                }
            }
        }
        //If the are no object to use, here we can create more, or just return null
        else
        {

        }
        return null; // TODO: Remove this later, the null return was already called
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

    private void SetEnemiesIndexList()
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
}

[System.Serializable]
public class PoolingObject
{
    public GameObject Prefab;
    public int AmountToSpawn;
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
