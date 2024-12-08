using System.Collections;
using UnityEngine;

public class Flame : MonoBehaviour
{
    [SerializeField] private GameObject colObj;
    [SerializeField] private LayerMask enemyLayer;

    private readonly float arrangeTime = 5f;
    private readonly float damageInterval = 0.5f;

    private Animator animator;
    private PlayerSystem playerSystem;

    private WaitForSeconds waitArrangeTime;
    private WaitForSeconds waitDamageInterval;

    private void OnEnable()
    {
        InitValues();
        StartCoroutine(FlamingCoroutine());
        StartCoroutine(AttackEnemiesCoroutine());
    }

    private void InitValues()
    {
        animator = GetComponent<Animator>();
        playerSystem = FindObjectOfType<PlayerSystem>();

        waitArrangeTime = new(arrangeTime);
        waitDamageInterval = new(damageInterval);
    }

    private IEnumerator FlamingCoroutine()
    {
        yield return WaitForAnimationState("FlameStart", 1f);
        animator.SetTrigger("Start");

        yield return waitArrangeTime;

        yield return WaitForAnimationState("Flaming", 1f);
        animator.SetTrigger("End");

        yield return WaitForAnimationState("FlameEnd", 1f);
        ObjectPoolManager.Instance.ReleaseToPool(gameObject);
    }

    private IEnumerator WaitForAnimationState(string stateName, float _normalized)
    {
        while (true)
        {
            var state = animator.GetCurrentAnimatorStateInfo(0);

            if (state.IsName(stateName) && state.normalizedTime >= _normalized)
            {
                break;
            }

            yield return null;
        }
    }

    private IEnumerator AttackEnemiesCoroutine()
    {
        while (true)
        {
            var collider = colObj.GetComponent<BoxCollider2D>();

            if (collider != null)
            {
                Vector2 center = (Vector2)colObj.transform.position + collider.offset;
                Vector2 size = collider.size;

                Collider2D[] hits = Physics2D.OverlapBoxAll(center, size, 0f, enemyLayer);

                foreach (var hit in hits)
                {
                    var health = hit.GetComponent<BaseHealth>();
                    health.Hit(playerSystem.GetAttack() * 2);
                }

                yield return waitDamageInterval;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (colObj != null)
        {
            var collider = colObj.GetComponent<BoxCollider2D>();
            if (collider != null)
            {
                Gizmos.color = Color.red;
                Vector2 center = (Vector2)colObj.transform.position + collider.offset;
                Vector2 size = collider.size;
                Gizmos.DrawWireCube(center, size); // 디버그용 탐지 범위 표시
            }
        }
    }
}
