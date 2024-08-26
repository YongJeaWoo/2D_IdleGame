using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerAttack : BaseAttack
{
    [SerializeField] private Transform attackPos;
    [SerializeField] private GameObject weapon;

    private SpeedComponent speed;
    private BackgroundController bgController;
    
    protected override void Awake()
    {
        base.Awake();
        GetComponents();
    }

    private void Start()
    {
        ObjectPoolManager.Instance.InitObjectPool(weapon);
    }

    private void GetComponents()
    {
        speed = GetComponent<SpeedComponent>();
        bgController = FindAnyObjectByType<BackgroundController>();

        UIManager.Instance.InitHpImage();
    }

    protected override void DetectEnemy()
    {
        Vector2 rayPos = new(transform.position.x, transform.position.y + 0.25f);
        RaycastHit2D[] hits = Physics2D.RaycastAll(rayPos, Vector2.right, detectionDistance, enemyLayer);

        if (hits.Length > 0)
        {
            var nearByTarget = hits[0];

            DetectObject(true);
            RefreshTargetHp(nearByTarget);

            prevTarget = nearByTarget;
        }
        else
        {
            DetectObject(false);
            RefreshTargetHp(prevTarget);
        }
    }

    protected override void RefreshTargetHp(RaycastHit2D? target)
    {
        base.RefreshTargetHp(target);

        UIManager.Instance.RefreshHpBar(null, 0, 0);
    }

    private void DetectObject(bool isAttack)
    {
        var m_speed = speed.GetSpeed();
        animator.speed = m_speed;

        animator.SetBool("isRun", !isAttack);
        animator.SetBool("isAttack", isAttack);

        bgController.BG_Controll(isAttack);
    }

    public override void AttackAnimation()
    {
        ObjectPoolManager.Instance.GetToPool(weapon, attackPos);
    }
}
