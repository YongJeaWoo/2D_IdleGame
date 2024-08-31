using System.Numerics;
using UnityEngine;

public class KnifeAttack : MonoBehaviour
{
    [SerializeField] private string attackPointString;
    private BigInteger attackPoint;

    private void OnEnable()
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

    private void Release()
    {
        ObjectPoolManager.Instance.ReleaseToPool(gameObject);
    }

    public BigInteger GetAttackPoint() => attackPoint;

    public BigInteger SetAttackPoint(BigInteger value) => attackPoint = value;
}
