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

    protected override void DetectEnemy()
    {
        UnityEngine.Vector2 rayPos = new(transform.position.x, transform.position.y + 0.2f);
        RaycastHit2D[] hits = Physics2D.RaycastAll(rayPos, UnityEngine.Vector2.left, detectionDistance, enemyLayer);

        if (hits.Length > 0)
        {
            RefreshTargetHp(hits[0]);
        }
    }

    public override void AttackAnimation()
    {
        
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
