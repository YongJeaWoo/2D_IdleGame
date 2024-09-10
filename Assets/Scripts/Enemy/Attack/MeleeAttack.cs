using System.Numerics;
using UnityEngine;

public class MeleeAttack : BaseAttack
{
    [SerializeField] private string attackPointString;
    private BigInteger attackPoint;

    protected void OnEnable()
    {
        attackPoint = BigInteger.Parse(attackPointString);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var health = collision.GetComponent<BaseHealth>();
            health.Hit(attackPoint);
        }
    }
}
