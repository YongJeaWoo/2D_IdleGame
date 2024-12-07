using System.Collections;
using UnityEngine;

public class Posion : MonoBehaviour
{
    [SerializeField] private GameObject colObj;

    private readonly float arrangeTime = 10f;
    private readonly float damagedInterval = 1f;

    private Animator animator;
    private Rigidbody2D rb;
    private bool hasTriggered = false;

    private PlayerSystem playerSystem;

    private WaitForSeconds waitArrangeTime;
    private WaitForSeconds waitDamageInterval;

    private bool isDamaged = true;

    private void Awake()
    {
        GetComponents();
    }

    private void OnDisable()
    {
        hasTriggered = false;
        ChangeColliderArea(false);
    }

    private void GetComponents()
    {
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
        playerSystem = FindObjectOfType<PlayerSystem>();

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnAttackEnemy(collision);
    }

    private void OnAttackEnemy(Collider2D collider)
    {
        if (collider.CompareTag("Enemy") && isDamaged)
        {
            var health = collider.GetComponent<BaseHealth>();
            health.Hit(playerSystem.GetAttack() * 2);
            StartCoroutine(DamageCooldownCoroutine());
        }
    }

    private IEnumerator DamageCooldownCoroutine()
    {
        isDamaged = false;
        yield return waitDamageInterval;
        isDamaged = true;
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
        animator.SetTrigger("isGround");
        yield return WaitForNextAnimation("Spread");
        ChangeColliderArea(true);

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

    private void ChangeColliderArea(bool isOn)
    {
        colObj.SetActive(isOn);
    }
}
