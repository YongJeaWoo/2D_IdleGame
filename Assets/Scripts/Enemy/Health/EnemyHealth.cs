using System;
using System.Numerics;
using UnityEngine;

public class EnemyHealth : BaseHealth
{
    public event Action OnDeath;

    protected override void Start()
    {
        SetValues();
        base.Start();
    }

    protected void OnEnable()
    {
        IncreaseRoundToValue();
    }

    protected override void Death()
    {
        base.Death();
        OnDeath?.Invoke();
    }

    private void SetValues()
    {
        myHealthBar = UIManager.Instance.GetHpBars()[1];
        myHealthText = UIManager.Instance.GetHpTexts()[1];
    }

    public override void IncreaseRoundToValue()
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
