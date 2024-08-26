using System;
using System.Numerics;
using UnityEngine;
using static UnityEditor.Progress;

public class EnemyHealth : BaseHealth
{
    public event Action OnDeath;
    private Item item;

    protected override void Start()
    {
        base.Start();
        item = GetComponent<Item>();
    }

    protected void OnEnable()
    {
        IncreaseHealthToRound();
        SetValues();
    }

    protected override void Death()
    {
        base.Death();
        item.Drop();
        OnDeath?.Invoke();
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
