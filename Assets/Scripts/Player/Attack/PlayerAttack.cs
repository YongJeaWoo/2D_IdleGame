using System.Numerics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerAttack : BaseAttack
{
    [SerializeField] private Transform attackPos;
    [SerializeField] private GameObject weapon;

    private SpeedComponent speed;
    private BackgroundController bgController;

    [SerializeField] private string atkString;
    private BigInteger atk;

    protected override void Awake()
    {
        base.Awake();
        GetComponents();
        atk = BigInteger.Parse(atkString);
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
        UnityEngine.Vector2 rayPos = new(transform.position.x, transform.position.y + 0.25f);
        RaycastHit2D[] hits = Physics2D.RaycastAll(rayPos, UnityEngine.Vector2.right, detectionDistance, enemyLayer);

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
        animator.speed = m_speed * 0.5f;

        animator.SetBool("isRun", !isAttack);
        animator.SetBool("isAttack", isAttack);

        bgController.BG_Controll(isAttack);
    }

    public override void AttackAnimation()
    {
        GameObject knife = ObjectPoolManager.Instance.GetToPool(weapon, attackPos);

        if (knife != null)
        {
            var knifeAkt = knife.GetComponent<KnifeAttack>().GetAttackPoint();
            var totalAkt = knifeAkt + atk;
            knife.GetComponent<KnifeAttack>().SetAttackPoint(totalAkt);
        }
    }

    public BigInteger GetAtk() => atk;
    public BigInteger SetAtk(BigInteger value) => atk = value;
}
