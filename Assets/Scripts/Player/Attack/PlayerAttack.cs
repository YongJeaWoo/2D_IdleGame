using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TMPro.Examples;
using UnityEngine;

public class PlayerAttack : BaseAttack
{
    private readonly string runText = $"isRun";
    private readonly string attackText = $"isAttack";

    [Header("공격 사운드")]
    [SerializeField] private AudioClip[] attackSound;

    [SerializeField] private Transform attackPos;

    private SpeedComponent speed;
    private BackgroundController bgController;
    private KnifeCollectionBar knifeBar;

    [SerializeField] private List<GameObject> attackKnifes;
    private List<GameObject> sortedKnifes = new List<GameObject>();

    [Header("공격력")]
    [SerializeField] private string atkString;
    private BigInteger atk;

    private int currentKnifeIndex = 0;
    private bool isAttack;

    protected override void Awake()
    {
        base.Awake();
        GetComponents();
    }

    private void Start()
    {
        atk = BigInteger.Parse(atkString);
        StartAttack();
    }

    protected void OnEnable()
    {
        knifeBar.OnUpdateKnife += GetKnifeInfo;
    }

    private void OnDisable()
    {
        knifeBar.OnUpdateKnife += GetKnifeInfo;
    }

    private void GetComponents()
    {
        speed = GetComponent<SpeedComponent>();
        bgController = FindAnyObjectByType<BackgroundController>();
        UIManager.Instance.InitHpImage();
        knifeBar = UIManager.Instance.gameObject.GetComponentInChildren<KnifeCollectionBar>();

        LoadKnifesToPlayerAttack();

        foreach (var knife in attackKnifes)
        {
            ObjectPoolManager.Instance.InitObjectPool(knife);
        }
    }

    protected override void DetectObject()
    {
        if (!isAttack)
        {
            animator.SetBool(runText, true);
            return;
        }

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

    private void LoadKnifesToPlayerAttack()
    {
        var knifeList = knifeBar.GetKnifesList();

        var knifeListCount = knifeList.GroupBy(k => k.GetComponent<KnifeNextData>().NextID)
                                      .ToDictionary(group => group.Key, group => group.Count());

        var matchingKnifes = new List<GameObject>();

        foreach (var knife in attackKnifes)
        {
            var knifeNextData = knife.GetComponent<KnifeNextData>();
            if (knifeNextData != null && knifeListCount.ContainsKey(knifeNextData.NextID))
            {
                int count = knifeListCount[knifeNextData.NextID];

                for (int i = 0; i < count; i++)
                {
                    matchingKnifes.Add(knife);
                }
            }
        }

        sortedKnifes = matchingKnifes
            .OrderByDescending(knife => BigInteger.Parse(
                knife.GetComponent<KnifeAttack>().GetAttackPointString()))
            .ToList();
    }

    public void GetKnifeInfo()
    {
        var knifeList = knifeBar.GetKnifesList();

        var knifeListCount = knifeList.GroupBy(k => k.GetComponent<KnifeNextData>().NextID)
                                  .ToDictionary(group => group.Key, group => group.Count());

        var matchingKnifes = new List<GameObject>();

        foreach (var knife in attackKnifes)
        {
            var knifeNextData = knife.GetComponent<KnifeNextData>();
            if (knifeNextData != null && knifeListCount.ContainsKey(knifeNextData.NextID))
            {
                int count = knifeListCount[knifeNextData.NextID];

                for (int i = 0; i < count; i++)
                {
                    matchingKnifes.Add(knife);
                }
            }
        }

        sortedKnifes = matchingKnifes
            .OrderByDescending(knife => BigInteger.Parse(
                knife.GetComponent<KnifeAttack>().GetAttackPointString()))
            .ToList();
    }

    public override void AttackAnimation()
    {
        if (sortedKnifes.Count == 0) return;

        if (currentKnifeIndex >= sortedKnifes.Count)
        {
            currentKnifeIndex = 0;
        }

        var currentKnife = sortedKnifes[currentKnifeIndex];

        if (currentKnife == null)
        {
            Debug.LogError("currentKnife is null, cannot get to pool!");
            return;
        }

        ObjectPoolManager.Instance.GetToPool(currentKnife, attackPos);

        if (attackSound != null && attackSound.Length > 0)
        {
            int randomSound = Random.Range(0, attackSound.Length);
            AudioManager.Instance.PlaySFX(attackSound[randomSound]);
        }

        currentKnifeIndex++;
    }

    public BigInteger GetAtk() => atk;
    public BigInteger SetAtk(BigInteger value)
    {
        atk = value;
        atkString = atk.ToString();
        return atk;
    }

    public void StartAttack()
    {
        isAttack = true;
    }

    public void StopAttack()
    {
        isAttack = false;

        animator.SetBool(runText, true);
        animator.SetBool(attackText, false);
    }

    public Transform GetAttackPos() => attackPos;
}
