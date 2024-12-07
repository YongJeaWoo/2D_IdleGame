using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    [SerializeField] private GameObject colObj;

    private readonly float damageInterval = 0.3f;
    private readonly float arrangeTime = 5f;
    private Animator animator;
    private bool isDamaged = true;

    private PlayerSystem playerSystem;

    private WaitForSeconds waitArrangeTime;
    private WaitForSeconds waitDamageInterval;

    private void OnEnable()
    {
        InitValues();
        StartCoroutine(FlamingCoroutine());
    }

    private void OnDisable()
    {
        ChangeColliderArea(false);
    }


    private void InitValues()
    {
        animator = GetComponent<Animator>();
        playerSystem = FindObjectOfType<PlayerSystem>();

        waitArrangeTime = new WaitForSeconds(arrangeTime);
        waitDamageInterval = new WaitForSeconds(damageInterval);
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

    private IEnumerator WaitForAnimationState(string stateName, float _normalizedTime)
    {
        while (true)
        {
            var state = animator.GetCurrentAnimatorStateInfo(0);

            if (state.IsName(stateName) && state.normalizedTime >= _normalizedTime)
            {
                break;
            }

            yield return null;
        }
    }

    private void OnAttackEnemy(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            ChangeColliderArea(true);

            if (isDamaged)
            {
                var health = collider.GetComponent<BaseHealth>();
                health.Hit(playerSystem.GetAttack());
                StartCoroutine(DamageCooldownCoroutine());
            }
        }
    }

    private IEnumerator DamageCooldownCoroutine()
    {
        isDamaged = false;
        yield return waitDamageInterval;
        isDamaged = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnAttackEnemy(collision);
    }

    private void ChangeColliderArea(bool isOn)
    {
        colObj.SetActive(isOn);
    }
}
