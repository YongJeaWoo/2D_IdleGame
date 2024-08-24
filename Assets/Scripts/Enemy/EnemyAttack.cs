using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class EnemyAttack : BaseAttack
{
    [SerializeField] private string attackPointString;
    private BigInteger attackPoint;
    private Animator animator;
    private Rigidbody2D rb;
    [SerializeField] private float knockbackForce = 3f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public override void AttackAnimation()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var health = collision.GetComponent<BaseHealth>();
            health.Hit(attackPoint);

            Debug.Log(health.currentHp);

            UnityEngine.Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

            // 1초 뒤에 리지드 바디의 움직임을 멈추는 코루틴 시작
            StartCoroutine(StopRigidbodyMovement());
        }
    }

    private IEnumerator StopRigidbodyMovement()
    {
        yield return new WaitForSeconds(0.5f);
        rb.velocity = UnityEngine.Vector2.zero;
        rb.angularVelocity = 0f;
    }
}
