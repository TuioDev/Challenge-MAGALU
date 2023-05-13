using System;
using System.Collections;
using System.Collections.Generic;
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

    public List<SpikeBehaviour> PooledObjects;
    public GameObject ObjectToPool;
    public int AmountToPool;

    private void Awake()
    {
        PooledObjects = new List<SpikeBehaviour>();
        
        //Instantiates all objects that the game can use
        InstatiateSpikeBehaviours();
    }

    private void InstatiateSpikeBehaviours()
    {
        SpikeBehaviour tmp;
        for (int i = 0; i < AmountToPool; i++)
        {
            tmp = Instantiate(ObjectToPool.GetComponent<SpikeBehaviour>());
            tmp.gameObject. SetActive(false);
            PooledObjects.Add(tmp);
        }
    }

    public SpikeBehaviour GetInactivePooledObject()
    {
        for (int i = 0; i < AmountToPool; i++)
        {
            if (!PooledObjects[i].gameObject.activeInHierarchy)
            {
                return PooledObjects[i];
            }
        }

        //If the are no object to use, here we can create more, or just return null
        return null;
    }
}
