using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<PoolItem> items;
    public List<GameObject> pooledItems;

    private void Start()
    {
        pooledItems = new List<GameObject>();

        foreach (PoolItem item in items)
        {
            for (int i = 0; i < item.amount; i++)
            {
                GameObject instantiatedObject = Instantiate(item.prefab);
                instantiatedObject.SetActive(false);
                pooledItems.Add(instantiatedObject);
            }
        }
    }

    public GameObject GetGameObject(string tag)
    {
        for (int i = 0; i < pooledItems.Count; i++)
        {
            if (!pooledItems[i].activeInHierarchy && pooledItems[i].CompareTag(tag))
            {
                return pooledItems[i].gameObject;
            }
        }

        foreach (PoolItem item in items)
        {
            if (item.prefab.CompareTag(tag))
            {
                GameObject instantiatedObject = Instantiate(item.prefab);
                instantiatedObject.SetActive(false);
                pooledItems.Add(instantiatedObject);
                return instantiatedObject;
            }
        }

        return null;
    }
}

[Serializable]
public class PoolItem
{
    public GameObject prefab;
    public int amount;
}