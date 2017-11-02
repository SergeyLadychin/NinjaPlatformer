using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class ObjectGetter
{
    public GameObject objectPrefab;
    public ObjectPool objectPool;

    public GameObject Get(Vector3 position, Quaternion rotation)
    {
        if (objectPool != null)
        {
            return objectPool.GetObject(position, rotation);
        }
        else
        {
            return Object.Instantiate(objectPrefab, position, rotation);
        }
    }

    public T Get<T>(Vector3 position, Quaternion rotation)
    {
        var obj = Get(position, rotation);
        return obj.GetComponent<T>();
    }

    public bool IsFromPool()
    {
        return objectPool != null;
    }
}
