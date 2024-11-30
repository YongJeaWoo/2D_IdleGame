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
        DetectObject();
    }

    protected virtual void DetectObject()
    {
        Vector2 rayPos = new(transform.position.x, transform.position.y + 0.25f);
        RaycastHit2D[] hits = Physics2D.RaycastAll(rayPos, Vector2.left, detectionDistance, enemyLayer);

        if (hits.Length > 0)
        {
            var nearByTarget = hits[0];
            RefreshTargetHp(nearByTarget);
            UIManager.Instance.GetNameText()[0].text = nearByTarget.collider.gameObject.name;
        }
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
