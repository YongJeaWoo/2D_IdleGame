using System.Collections;
using UnityEngine;

public class MonsterMovement : BaseMovement
{
    [SerializeField] private float knockbackForce = 3f;

    protected Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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
  