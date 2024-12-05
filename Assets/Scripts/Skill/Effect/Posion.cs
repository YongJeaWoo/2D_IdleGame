using System.Collections;
using UnityEngine;

public class Posion : MonoBehaviour
{
    private readonly float arrangeTime = 10f;

    private Animator animator;
    private Rigidbody2D rb;
    private bool hasTriggered = false;

    private void Awake()
    {
        GetComponents();
    }

    private void GetComponents()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.CompareTag("Ground"))
        {
            hasTriggered = true;
            TriggerOnGround(collision);
        }
    }

    private void TriggerOnGround(Collider2D collision)
    {
        Vector2 groundPosition = collision.ClosestPoint(transform.position);
        transform.position = groundPosition;

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }

        animator.SetTrigger("isGround");
        StartCoroutine(HandleNapalmAnimation());
    }

    private IEnumerator HandleNapalmAnimation()
    {
        yield return WaitForNextAnimation("isGround");

        animator.SetTrigger("Napalm");

        yield return new WaitForSeconds(arrangeTime);

        if (rb != null)
        {
            rb.isKinematic = false; 
        }

        ObjectPoolManager.Instance.ReleaseToPool(gameObject);
    }

    private IEnumerator WaitForNextAnimation(string currentStateName)
    {
        while (animator.GetCurrentAnimatorStateInfo(0).IsName(currentStateName))
        {
            yield return null;
        }
    }
}
