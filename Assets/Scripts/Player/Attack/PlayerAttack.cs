using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;

public class PlayerAttack : BaseAttack
{
    private readonly string runText = $"isRun";
    private readonly string attackText = $"isAttack";


    [SerializeField] private Transform attackPos;

    [Header("실제 공격할 칼 정보")]
    [SerializeField] private List<GameObject> knifesInfos;

    private SpeedComponent speed;
    private BackgroundController bgController;
    private KnifeCollectionBar knifeBar;

    private List<GameObject> knifes;
    private List<GameObject> sortedKnifes = new List<GameObject>();

    [Header("공격력")]
    [SerializeField] private string atkString;
    private BigInteger atk;

    protected override void Awake()
    {
        base.Awake();
        GetComponents();
        atk = BigInteger.Parse(atkString);
    }

    protected void OnEnable()
    {
        CreateKnifeButton.OnKnifeCreated += GetknifeInfo;
    }

    protected void OnDisable()
    {
        CreateKnifeButton.OnKnifeCreated -= GetknifeInfo;
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
        if (sortedKnifes.Count == 0)
        {
            animator.SetBool(runText, true);
            return;
        }

        var m_speed = speed.GetSpeed();
        animator.speed = m_speed * 0.5f;

        animator.SetBool(runText, !isAttack);
        animator.SetBool(attackText, isAttack);

        bgController.BG_Controll(isAttack);
    }

    public void GetknifeInfo()
    {
        knifeBar = UIManager.Instance.gameObject.GetComponentInChildren<KnifeCollectionBar>();
        knifes = knifeBar.GetAttackKnifes();

        sortedKnifes = knifes
            .Select(knife =>
            {
                return knifesInfos.FirstOrDefault(info => info.name == knife.name);
            })
            .Where(info => info != null) .ToList();

        foreach (var knife in sortedKnifes)
        {
            if (!ObjectPoolManager.Instance.IsPoolInitialized(knife))
            {
                ObjectPoolManager.Instance.InitObjectPool(knife);
            }
        }
    }

    private int currentKnifeIndex = 0;

    public override void AttackAnimation()
    {
        if (sortedKnifes.Count == 0) return;

        sortedKnifes = sortedKnifes
            .OrderByDescending(knife =>
            {
                var knifeAttack = knife.GetComponent<KnifeAttack>();

                if (knifeAttack != null)
                {
                    var stringAtk = knifeAttack.GetAttackPointString();
                    return BigInteger.Parse(stringAtk);
                }

                return BigInteger.Zero;
            })
            .ToList();

        GameObject matchKnifeInfo = sortedKnifes[currentKnifeIndex];
        var knife = ObjectPoolManager.Instance.GetToPool(matchKnifeInfo, attackPos);

        if (knife == null) return;

        var knifeAkt = knife.GetComponent<KnifeAttack>().GetAttackPoint();
        var totalAkt = knifeAkt + atk;
        knife.GetComponent<KnifeAttack>().SetAttackPoint(totalAkt);

        currentKnifeIndex = (currentKnifeIndex + 1) % sortedKnifes.Count;
    }

    public BigInteger GetAtk() => atk;
    public BigInteger SetAtk(BigInteger value) => atk = value;
}
