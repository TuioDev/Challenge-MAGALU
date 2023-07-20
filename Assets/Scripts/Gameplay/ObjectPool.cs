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

    [Header("ALL OBJECTS TO POOL")]
    [Header("Spikes")]
    [SerializeField] private SpikeBehaviour SpikePrefab;
    [SerializeField] private int AmountOfSpikeToPool;
    [Header("Ants")]
    [SerializeField] private Ant AntPrefab;
    [SerializeField] private int AmountOfAntsToPool;
    [Header("Spiders")]
    [SerializeField] private Spider SpiderPrefab;
    [SerializeField] private int AmountOfSpidersToPool;

    private void Awake()
    {
        //Instantiates all objects that the game can use
        InstantiateComponents<SpikeBehaviour>(SpikePrefab, AmountOfSpikeToPool);
        InstantiateComponents<Spider>(SpiderPrefab, AmountOfSpidersToPool);
        InstantiateComponents<Ant>(AntPrefab, AmountOfAntsToPool);
    }

    private void InstantiateComponents<T>(T componentToPool, int amountToPool) where T : Component
    {
        ListOfSameObject subList = new();

        for (int i = 0; i < amountToPool; i++)
        {
            T tmp = Instantiate(componentToPool);
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
        //If the are no object to use, here we can create more, or just return null
        else
        {
            
        }
        return null;
    }

    // TODO: Change this method so that it supports all lists of enemies?
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
