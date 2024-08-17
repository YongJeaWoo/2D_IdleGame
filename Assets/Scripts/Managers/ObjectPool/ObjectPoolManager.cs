using SingletonBase.DestroySingleton;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolManager : SingletonBase<ObjectPoolManager>
{
    private List<GameObject> poolObjs = new();

    public void InitObjectPool(GameObject poolObj, ref ObjectPool usePool)
    {
        GameObject poolingObj = new(poolObj.name);
        poolingObj.transform.SetParent(transform);
        poolObjs.Add(poolingObj);
        usePool = poolingObj.AddComponent<ObjectPool>();

        if (usePool != null)
        {
            usePool.SetPoolObject(poolObj);
        }
    }

    public ObjectPool FindObjectPool(GameObject findPool)
    {
        string cleanName = findPool.name.Replace("(Clone)", "").Trim();
        var poolObj = poolObjs.FirstOrDefault(n => n.name == cleanName);

        if (poolObj != null)
        {
            var objectPool = poolObj.GetComponent<ObjectPool>();
            return objectPool;
        }
        else
        {
            return null;
        }
    }
}