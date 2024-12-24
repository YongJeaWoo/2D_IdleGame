using System;
using System.Numerics;
using UnityEngine;

public class EnemyHealth : BaseHealth
{
    public event Action OnDeath;

    protected void OnEnable()
    {
        IncreaseHealthToRound();
        SetValues();
    }

    protected override void Death()
    {
        base.Death();
        var droppers = GetComponents<DropPossessItem>();
        foreach (var dropper in droppers)
        {
            dropper.DropItem();
        }
        // 재화 이벤트 등록
        OnDeath?.Invoke();
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
