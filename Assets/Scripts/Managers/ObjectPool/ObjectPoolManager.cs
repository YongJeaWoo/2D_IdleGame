using SingletonBase.DestroySingleton;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : SingletonBase<ObjectPoolManager>
{
    private Dictionary<GameObject, ObjectPool> poolDict = new();

    public void InitObjectPool(GameObject poolObj)
    {
        if (poolDict.ContainsKey(poolObj)) return;

        GameObject poolingObj = new GameObject(poolObj.name);
        poolingObj.transform.SetParent(transform);

        var pool = poolingObj.AddComponent<ObjectPool>();
        pool.SetPoolObject(poolObj);
        poolDict[poolObj] = pool;
    }

    public GameObject GetToPool(GameObject poolObj, Transform pos = null)
    {
        var pool = GetPool(poolObj);

        if (pool != null)
        {
            var obj = pool.GetPoolObject(pool.transform);
            obj.transform.position = pos.position;
            return obj;
        }

        return null;
    }

    public void ReleaseToPool(GameObject poolObj)
    {
        string cleanName = poolObj.name.Replace("(Clone)", "").Trim();

        foreach (var pool in poolDict)
        {
            if (pool.Key.name == cleanName)
            {
                pool.Value.ReleasePoolObject(poolObj);
                break;
            }
        }
    }
    
    private ObjectPool GetPool(GameObject poolObj)
    {
        poolDict.TryGetValue(poolObj, out var pool);
        return pool;
    }

}