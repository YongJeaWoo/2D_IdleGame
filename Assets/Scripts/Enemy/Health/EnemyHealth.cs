using System;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : BaseHealth
{
    [SerializeField] private GameObject[] dropItems;

    public event Action OnDeath;

    protected override void Start()
    {
        base.Start();
    }

    protected void OnEnable()
    {
        IncreaseHealthToRound();
        SetValues();
    }

    protected override void Death()
    {
        base.Death();
        // ��ȭ �̺�Ʈ ���
        UIManager.Instance.OnDrop += HandleDrop;
        OnDeath?.Invoke();
    }

    private void OnDisable()
    {
        // ��ȭ �̺�Ʈ ����
        UIManager.Instance.OnDrop -= HandleDrop;
    }

    private void HandleDrop(IItemDropper itemDropper)
    {
        if (dropItems.Length == 0) return;

        int randomIndex = UnityEngine.Random.Range(0, dropItems.Length);
        GameObject itemToDrop = Instantiate(dropItems[randomIndex], transform.position, UnityEngine.Quaternion.identity);

        itemDropper = itemToDrop.GetComponent<IItemDropper>();
        itemDropper?.DropItem(); // ������ ���
    }


    private void SetValues()
    {
        myHealthBar = UIManager.Instance.GetHpBars()[1];
        myHealthText = UIManager.Instance.GetHpTexts()[1];
    }

    private void IncreaseHealthToRound()
    {
        var round = LevelManager.Instance.GetCurrentRound();

        int ceilRoundHp = (int)(Mathf.Ceil(round * 1.4f));

        BigInteger previousMaxHp = BigInteger.Parse(maxHpString);
        BigInteger calHp = previousMaxHp + (round * ceilRoundHp);
        maxHp = calHp;

        maxHpString = maxHp.ToString();
        SetCurrentHpToMaxHp();
    }
}
