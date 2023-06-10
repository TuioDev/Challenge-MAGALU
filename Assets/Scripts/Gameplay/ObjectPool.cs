using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using static Unity.Burst.Intrinsics.X86.Avx;

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

    public List<ListOfSameObject> AllLists = new List<ListOfSameObject>();
    public List<GameObject> PooledObjects = new List<GameObject>();

    [Header("All objects to pool")]
    [SerializeField] private SpikeBehaviour SpikePrefab;
    [SerializeField] private int AmountOfSpikeToPool;
    [SerializeField] private Enemy EnemyPrefab;
    [SerializeField] private int AmountOfEnemyToPool;

    private void Awake()
    {
        //Instantiates all objects that the game can use
        InstantiateComponents<SpikeBehaviour>(SpikePrefab, AmountOfSpikeToPool);
        InstantiateComponents<Enemy>(EnemyPrefab, AmountOfEnemyToPool);
    }

    private void InstantiateComponents<T>(T componentToPool, int amountToPool) where T : Component
    {
        ListOfSameObject subList = new();
        for (int i = 0; i < amountToPool; i++)
        {
            T tmp = Instantiate(componentToPool);
            if (tmp is SpikeBehaviour) tmp.gameObject.name = "Spike " + i;
            subList.List.Add(tmp);
        }
        AllLists.Add(subList);
    }

    public T GetInactivePooledObject<T>() where T : Component
    {
        int index = GetListIndex<T>();

        if (AllLists[index] != null)
        {
            List<Component> subList = AllLists[index].List;
            for (int i = 0; i < subList.Count; i++)
            {
                if (!subList[i].gameObject.activeInHierarchy)
                {
                    return subList[i].gameObject.GetComponent<T>();
                }
            }
        }
        //for (int i = 0; i < AmountToPool; i++)
        //{
        //if (!PooledObjects[i].gameObject.activeInHierarchy)
        //{
        //    return PooledObjects[i];
        //}
        //}

        //If the are no object to use, here we can create more, or just return null
        return null;
    }

    private int GetListIndex<T>() where T : Component
    {
        for (int i = 0; i < AllLists.Count; i++)
        {
            if (AllLists[i].List[0] is T)
            {
                return i;
            }
        }
        return -1;
    }
}

[Serializable]
public class ListOfSameObject
{
    public List<Component> List;

    public ListOfSameObject()
    {
        List = new List<Component>();
    }
}
