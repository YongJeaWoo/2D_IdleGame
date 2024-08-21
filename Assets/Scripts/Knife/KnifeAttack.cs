using System.Numerics;
using UnityEngine;

public class KnifeAttack : MonoBehaviour
{
    [SerializeField] private string attackPointString;
    private BigInteger attackPoint;
    private ObjectPool pool;

    private void Start()
    {
        attackPoint = BigInteger.Parse(attackPointString);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            var health = collision.GetComponent<BaseHealth>();
            health.Hit(attackPoint);
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
