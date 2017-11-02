using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private List<GameObject> objects;

    public GameObject prefab;
    public int poolSize;
    public bool willGrow;

    void Awake()
    {
        objects = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            var obj = Instantiate(prefab);
            obj.SetActive(false);
            objects.Add(obj);
        }
    }

    public GameObject GetObject(Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (!objects[i].activeInHierarchy)
            {
                objects[i].transform.position = position;
                objects[i].transform.rotation = rotation;
                objects[i].SetActive(true);

                return objects[i];
            }
        }

        if (willGrow)
        {
            var obj = Instantiate(prefab, position, rotation);
            objects.Add(obj);
            return obj;
        }

        return null;
    }

    public T GetObject<T>(Vector3 position, Quaternion roration)
    {
        var obj = GetObject(position, roration);
        return obj.GetComponent<T>();
    }
}
