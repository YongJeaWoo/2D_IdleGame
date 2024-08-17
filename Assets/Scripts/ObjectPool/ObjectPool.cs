using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    private GameObject poolObject;
    private ObjectPool<GameObject> poolList;

    private int m_defaultCapacity = 10;
    private int m_maxSize = 30;

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
        return Instantiate(poolObject);
    }

    private void OnTakeFromPool(GameObject _poolObject)
    {
        _poolObject.SetActive(true);
    }

    private void  OnReturnedToPool(GameObject _poolObject)
    {
        _poolObject.SetActive(false);
    }

    private void OnDestroyPoolObject(GameObject _poolObject)
    {
        Destroy(_poolObject);
    }
    #endregion

    #region Use Pool
    public GameObject GetPoolObject()
    {
        return poolList.Get();
    }

    public void ReleasePoolObject(GameObject _poolObject)
    {
        poolList.Release(_poolObject);
    }
    #endregion

    public GameObject SetPoolObject(GameObject poolObj)
    {
        PoolInit();
        return poolObject = poolObj;
    }
}
