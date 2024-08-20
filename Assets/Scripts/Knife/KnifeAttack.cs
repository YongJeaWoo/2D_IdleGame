using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeAttack : MonoBehaviour
{
    private ObjectPool pool;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Release();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Background"))
        {
            Release();
        }
    }

    private void Release()
    {
        if(pool == null)
        {
            pool = ObjectPoolManager.Instance.FindObjectPool(gameObject);
        }

        pool.ReleasePoolObject(gameObject);
    }
}
