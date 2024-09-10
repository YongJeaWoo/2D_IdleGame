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
        knifeBar = UIManager.Instance.gameObject.GetComponentInChildren<KnifeCollectionBar>();
        sortedKnifes = knifeBar.GetAttackKnifes();

        sortedKnifes = sortedKnifes
            .Select(knife =>
            {
                var info = knifesInfos.FirstOrDefault(k => k.name == knife.name);

                if (info == null)
                {
                    Debug.LogError($"칼 정보를 찾지 못함");
                }

                return info;
            })
            .Where(info => info != null) .ToList();

        if (currentKnifeIndex >= sortedKnifes.Count)
        {
            currentKnifeIndex = 0;
        }

        sortedKnifes = sortedKnifes
            .OrderByDescending(knife =>
            {
                if (knife.TryGetComponent<KnifeAttack>(out var knifeAttack))
                {
                    var stringAtk = knifeAttack.GetAttackPointString();

                    if (BigInteger.TryParse(stringAtk, out var attackPoint))
                    {
                        return attackPoint;
                    }
                    else
                    {
                        return BigInteger.Zero;
                    }
                }
                else
                {
                    return BigInteger.Zero;
                }
            })
            .ToList();


        foreach (var knife in sortedKnifes)
        {
            ObjectPoolManager.Instance.InitObjectPool(knife);
        }
    }

    private int currentKnifeIndex = 0;

    public override void AttackAnimation()
    {
        if (sortedKnifes.Count == 0) return;

        GameObject matchKnifeInfo = sortedKnifes[currentKnifeIndex];
        var knife = ObjectPoolManager.Instance.GetToPool(matchKnifeInfo, attackPos);

        if (knife == null) return;

        var movement = knife.GetComponent<KnifeMovement>();
        
        if (movement == null)
        {
            knife.AddComponent<KnifeMovement>();
        }

        var knifeAkt = knife.GetComponent<KnifeAttack>().GetAttackPoint();
        var totalAkt = knifeAkt + atk;
        knife.GetComponent<KnifeAttack>().SetAttackPoint(totalAkt);

        currentKnifeIndex = (currentKnifeIndex + 1) % sortedKnifes.Count;
    }

    public BigInteger GetAtk() => atk;
    public BigInteger SetAtk(BigInteger value) => atk = value;
}
