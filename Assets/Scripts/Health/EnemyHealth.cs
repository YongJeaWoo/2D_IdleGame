using System;
using System.Numerics;
using UnityEngine;

public class EnemyHealth : BaseHealth
{
    [SerializeField] private GameObject deathParticle;

    public event Action OnDeath;

    protected void OnEnable()
    {
        IncreaseHealthToRound();
    }

    protected override void Death()
    {
        base.Death();
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
        currentHp = maxHp;
    }

    public override void Hit(BigInteger attackPoint)
    {
        base.Hit(attackPoint);
        UIManager.Instance.RefreshEnemyHp((int)currentHp, (int)maxHp);
    }
}
