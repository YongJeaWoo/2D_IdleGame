using System.Numerics;
using UnityEngine;

public class PlayerHealth : BaseHealth
{
    protected override void Start()
    {
        base.Start();
        SetValues();
    }

    protected void OnEnable()
    {
        InitializeHealth();
    }

    private void SetValues()
    {
        myHealthBar = UIManager.Instance.GetHpBars()[0];
        myHealthText = UIManager.Instance.GetHpTexts()[0];
    }

    private void InitializeHealth()
    {
        maxHp = BigInteger.Parse(maxHpString);
        SetCurrentHpToMaxHp();
    }

    protected override void Death()
    {
        // TODO : 페이드 처리 후 넣을 부분
        maxHp = BigInteger.Parse(maxHpString);
        SetCurrentHpToMaxHp();

        myHealthBar.fillAmount = (float)(double)currentHp / (float)(double)maxHp;

        // TODO : 페이드 아웃처리하고 그 라운드 재시작
    }

    public BigInteger SetMaxHp(BigInteger value)
    {
        maxHp = value;
        maxHpString = maxHp.ToString();
        return maxHp;
    }
}
