using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    private GameObject poolObject;
    private ObjectPool<GameObject> poolList;

    private int m_defaultCapacity = 10;
    private int m_maxSize = 30;

    private List<GameObject> pooledObjects = new List<GameObject>();

    private void PoolInit()
    {
        poolList = new ObjectPool<GameObject>
            (
                createFunc : CreatePooledItem,
                actionOnGet : OnTakeFromPool,
                actionOnRelease : OnReturnedToPool,
                actionOnDestroy : OnDestroyPoolObject,
                collectionCheck : true,
                defaultCapacity : m_defaultCapacity,
                maxSize : m_maxSize
            );
    }

    #region PoolObject
    private GameObject CreatePooledItem()
    {
        var obj = Instantiate(poolObject);
        pooledObjects.Add(obj);
        return obj;
    }

    private void OnTakeFromPool(GameObject _poolObject)
    {
        if (_poolObject == null) return;
        _poolObject.SetActive(true);
    }

    private void  OnReturnedToPool(GameObject _poolObject)
    {
        if (_poolObject == null) return;
        _poolObject.SetActive(false);
    }

    private void OnDestroyPoolObject(GameObject _poolObject)
    {
        if (_poolObject == null) return;
        Destroy(_poolObject);
        pooledObjects.Remove(_poolObject);
    }
    #endregion

    #region Use Pool
    public GameObject GetPoolObject(Transform parentPos = null)
    {
        GameObject obj;
        try
        {
            obj = poolList.Get();
        }
        catch
        {
            obj = CreatePooledItem();
        }

        if (obj == null || obj.Equals(null))
        {
            obj = CreatePooledItem();
        }

        if (parentPos != null)
        {
            obj.transform.SetParent(parentPos);
        }
        else
        {
            obj.transform.SetParent(null);
        }

        return obj;
    }

    public void ReleasePoolObject(GameObject _poolObject)
    {
        if (_poolObject.activeInHierarchy)
        {
            poolList.Release(_poolObject);
        }
    }

    public void ReleaseAllObjects()
    {
        List<GameObject> objectsToRelease = new(pooledObjects);

        foreach (var pool in objectsToRelease)
        {
            if (pool != null && pool.activeInHierarchy)
            {
                poolList.Release(pool);
            }
            else
            {
                pooledObjects.Remove(pool);
            }
        }
    }
    #endregion

    public GameObject SetPoolObject(GameObject poolObj)
    {
        if (poolList == null)
        {
            PoolInit();
        }

        poolObject = poolObj;
        return poolObject = poolObj;
    }
}
