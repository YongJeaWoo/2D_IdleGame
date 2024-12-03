using System.Numerics;
using UnityEngine;

public class KnifeAttack : MonoBehaviour
{
    [SerializeField] private string attackPointString;
    private BigInteger attackPoint;
    private BigInteger finalAttackPoint;
    private BigInteger playerAttack;

    private PlayerSystem playerSystem;

    private void Awake()
    {
        FindPlayerSystem();
    }

    private void FindPlayerSystem()
    {
        playerSystem = FindObjectOfType<PlayerSystem>();
    }

    private void OnEnable()
    {
        attackPoint = BigInteger.Parse(attackPointString);
        playerAttack = playerSystem.GetAttack();
        finalAttackPoint = attackPoint + playerAttack;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            var health = collision.GetComponent<BaseHealth>();
            health.Hit(finalAttackPoint);
            Release();
        }
    }

    private void Release()
    {
        ObjectPoolManager.Instance.ReleaseToPool(gameObject);
    }

    public string GetAttackPointString() => attackPointString;
    public BigInteger GetAttackPoint() => attackPoint;
}
