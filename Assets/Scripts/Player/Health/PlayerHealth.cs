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
        // TODO : ���̵� ó�� �� ���� �κ�
        maxHp = BigInteger.Parse(maxHpString);
        SetCurrentHpToMaxHp();

        myHealthBar.fillAmount = (float)(double)currentHp / (float)(double)maxHp;

        // TODO : ���̵� �ƿ�ó���ϰ� �� ���� �����
    }

    public BigInteger SetMaxHp(BigInteger value)
    {
        maxHp = value;
        maxHpString = maxHp.ToString();
        return maxHp;
    }
}
