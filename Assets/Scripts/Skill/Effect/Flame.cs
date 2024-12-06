using System.Collections;
using UnityEngine;

public class Flame : MonoBehaviour
{
    private readonly float arrangeTime = 5f;
    private Animator animator;
    private bool hasTriggered;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        hasTriggered = true;
        StartCoroutine(FlamingCoroutine());
    }

    private void OnDisable()
    {
        hasTriggered = false;
    }

    private IEnumerator FlamingCoroutine()
    {
        yield return WaitForAnimationState("FlameStart", 1f);
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(arrangeTime);

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
