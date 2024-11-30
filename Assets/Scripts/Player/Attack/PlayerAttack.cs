using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;

public class PlayerAttack : BaseAttack
{
    private readonly string runText = $"isRun";
    private readonly string attackText = $"isAttack";

    [SerializeField] private Transform attackPos;

    private SpeedComponent speed;
    private BackgroundController bgController;
    private KnifeCollectionBar knifeBar;

    [SerializeField] private List<GameObject> attackKnifes;
    private List<GameObject> sortedKnifes = new List<GameObject>();

    [Header("°ø°Ý·Â")]
    [SerializeField] private string atkString;
    private BigInteger atk;

    private int currentKnifeIndex = 0;

    protected override void Awake()
    {
        base.Awake();
        GetComponents();
    }

    private void Start()
    {
        atk = BigInteger.Parse(atkString);
    }

    protected void OnEnable()
    {
        KnifeCollectionBar.OnUpdateKnife += GetKnifeInfo;
    }

    protected void OnDisable()
    {
        KnifeCollectionBar.OnUpdateKnife -= GetKnifeInfo;
    }

    private void GetComponents()
    {
        speed = GetComponent<SpeedComponent>();
        bgController = FindAnyObjectByType<BackgroundController>();
        UIManager.Instance.InitHpImage();
        knifeBar = UIManager.Instance.gameObject.GetComponentInChildren<KnifeCollectionBar>();
    }

    protected override void DetectObject()
    {
        UnityEngine.Vector2 rayPos = new(transform.position.x, transform.position.y + 0.25f);
        RaycastHit2D[] hits = Physics2D.RaycastAll(rayPos, UnityEngine.Vector2.right, detectionDistance, enemyLayer);

        if (hits.Length > 0)
        {
            var nearByTarget = hits[0];
            var cleanName = nearByTarget.collider.gameObject.name.Replace("(Clone)", "").Trim();
            UIManager.Instance.GetNameText()[1].text = cleanName;

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

    public void GetKnifeInfo()
    {
        var knifeList = knifeBar.GetKnifesList();
        var matchingKnifes = attackKnifes.Where(knife =>
        {
            var knifeNextData = knife.GetComponent<KnifeNextData>();
            return knifeNextData != null && knifeList.Any(k => 
            k.GetComponent<KnifeNextData>().NextID == knifeNextData.NextID);
        });

        sortedKnifes = matchingKnifes
            .Select(knife => knife.GetComponent<KnifeAttack>())
            .OrderByDescending(knifeAttack => BigInteger.Parse(knifeAttack.GetAttackPointString()))
            .Select(knifeAttack => knifeAttack.gameObject)
            .ToList();

        foreach (var knife in sortedKnifes)
        {
            ObjectPoolManager.Instance.InitObjectPool(knife);
        }

        currentKnifeIndex = 0;
    }

    public override void AttackAnimation()
    {
        if (sortedKnifes.Count == 0) return;

        var currentKnife = sortedKnifes[currentKnifeIndex];
        ObjectPoolManager.Instance.GetToPool(currentKnife, attackPos);

        currentKnifeIndex = (currentKnifeIndex + 1) % sortedKnifes.Count;
    }

    public BigInteger GetAtk() => atk;
    public BigInteger SetAtk(BigInteger value)
    {
        atk = value;
        atkString = atk.ToString();
        return atk;
    }
}
