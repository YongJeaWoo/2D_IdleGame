using SingletonBase.DontDestroySingleton;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : SingletonBase<ObjectPoolManager>
{
    private Dictionary<GameObject, ObjectPool> poolDic = new();

    public void InitObjectPool(GameObject poolObj)
    {
        if (poolDic.ContainsKey(poolObj)) return;

        Transform existingPool = transform.Find(poolObj.name);

        ObjectPool pool;

        if (existingPool != null)
        {
            pool = existingPool.GetComponent<ObjectPool>();

            if (pool == null)
            {
                pool = existingPool.gameObject.AddComponent<ObjectPool>();
            }
        }
        else
        {
            GameObject poolingObj = new GameObject(poolObj.name);
            poolingObj.transform.SetParent(transform);

            pool = poolingObj.AddComponent<ObjectPool>();
        }

        pool.SetPoolObject(poolObj);
        poolDic[poolObj] = pool;
    }

    public GameObject GetToPool(GameObject poolObj, Transform createPos = null)
    {
        var pool = GetPool(poolObj);

        if (pool != null)
        {
            var obj = pool.GetPoolObject(createPos);
            
            if (createPos != null)
            {
                obj.transform.localPosition = Vector3.zero;
            }

            return obj;
        }

        return null;
    }

    public void ReleaseToPool(GameObject poolObj)
    {
        string cleanName = poolObj.name.Replace("(Clone)", "").Trim();

        foreach (var pool in poolDic)
        {
            if (pool.Key.name == cleanName)
            {
                pool.Value.ReleasePoolObject(poolObj);
                break;
            }
        }
    }

    public void ReleaseAllObjects()
    {
        foreach (var pool in poolDic)
        {
            if (!pool.Key.name.StartsWith("evo_"))
            {
                pool.Value.ReleaseAllObjects();
            }
        }
    }
    
    private ObjectPool GetPool(GameObject poolObj)
    {
        poolDic.TryGetValue(poolObj, out var pool);
        return pool;
    }
}