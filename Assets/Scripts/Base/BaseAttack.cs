using UnityEngine;

public class BaseAttack : MonoBehaviour
{
    [SerializeField] protected LayerMask enemyLayer;
    [SerializeField] protected float detectionDistance;

    protected BaseHealth targetHealth;
    protected Animator animator;
    protected RaycastHit2D? prevTarget = null;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        DetectEnemy();
    }

    protected virtual void DetectEnemy()
    {

    }

    protected virtual void RefreshTargetHp(RaycastHit2D? target)
    {
        if (target.HasValue)
        {
            var targetObj = target.Value.collider.gameObject;
            targetHealth = targetObj.GetComponent<BaseHealth>();
            UIManager.Instance.RefreshHpBar(targetHealth, targetHealth.GetCurrentHp(), targetHealth.GetMaxHp());
        }
    }

    public virtual void AttackAnimation()
    {

    }
}
