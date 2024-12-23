using System.Collections;
using UnityEngine;

public class Poison : MonoBehaviour
{
    [Header("이팩트 사운드")]
    [SerializeField] private AudioClip poisonEffectSound;

    [SerializeField] private GameObject colObj;
    [SerializeField] private LayerMask enemyLayer;

    private readonly float arrangeTime = 10f;
    private readonly float damagedInterval = 1f;

    private Animator animator;
    private Rigidbody2D rb;
    private bool hasTriggered = false;

    private WaitForSeconds waitArrangeTime;
    private WaitForSeconds waitDamageInterval;

    private void Awake()
    {
        GetComponents();
    }

    private void OnDisable()
    {
        hasTriggered = false;
    }

    private void GetComponents()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        waitArrangeTime = new WaitForSeconds(arrangeTime);
        waitDamageInterval = new WaitForSeconds(damagedInterval);
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
        
        StartCoroutine(HandleNapalmAnimation());
    }

    private IEnumerator HandleNapalmAnimation()
    {
        if (PlayerManager.Instance.GetPlayer().GetComponent<PlayerHealth>().GetIsPlayerDead())
        {
            StopAllCoroutines();
            AudioManager.Instance.StopSFX(poisonEffectSound);
            ObjectPoolManager.Instance.ReleaseToPool(gameObject);
            yield break;
        }

        animator.SetTrigger("isGround");
        yield return WaitForNextAnimation("Spread");

        animator.SetTrigger("Napalm");
        AudioManager.Instance.PlaySFX(poisonEffectSound, true);

        StartCoroutine(AttackEnemiesCoroutine());

        yield return waitArrangeTime;

        if (rb != null)
        {
            rb.isKinematic = false; 
        }

        ObjectPoolManager.Instance.ReleaseToPool(gameObject);
        AudioManager.Instance.StopSFX(poisonEffectSound);
    }

    private IEnumerator AttackEnemiesCoroutine()
    {
        while (true)
        {
            if (PlayerManager.Instance.GetPlayer().GetComponent<PlayerHealth>().GetIsPlayerDead())
            {
                StopAllCoroutines();
                AudioManager.Instance.StopSFX(poisonEffectSound);
                ObjectPoolManager.Instance.ReleaseToPool(gameObject);
                yield break;
            }

            var collider = colObj.GetComponent<BoxCollider2D>();

            if (collider != null)
            {
                Vector2 center = (Vector2)colObj.transform.position + collider.offset;
                Vector2 size = collider.size;

                Collider2D[] hits = Physics2D.OverlapBoxAll(center, size, 0f, enemyLayer);

                foreach (var hit in hits)
                {
                    var health = hit.GetComponent<BaseHealth>();
                    if (health != null)
                    {
                        health.Hit(PlayerManager.Instance.GetAttack());
                    }
                }

                yield return waitDamageInterval;
            }
        }
    }

    private IEnumerator WaitForNextAnimation(string currentStateName)
    {
        while (true)
        {
            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName(currentStateName) && stateInfo.normalizedTime >= 0.6f)
            {
                break; 
            }

            yield return null;
        }
    }
}
