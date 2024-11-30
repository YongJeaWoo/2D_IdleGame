using System;
using System.Numerics;
using UnityEngine;

public class MaxHpDataCell : DataCell
{
    private readonly float upgradeValue = 1.12f;
    private readonly string hpString = $"최대 체력 강화";

    protected override void OnEnable()
    {
        base.OnEnable();
        UpdateDisplay();
    }

    public override void InitInfo()
    {
        infoNameText.text = hpString;
        upgradeMultiple = upgradeValue;
    }

    public override void UpdateDisplay()
    {
        displayValue = playerSystem.GetMaxHp();

        decimal currentHp = (decimal)displayValue;
        decimal upgradeHp = currentHp * (decimal)upgradeMultiple;

        BigInteger newHp = new(Math.Ceiling(upgradeHp));

        numericalText.text = $"강화 시 \n{newHp}"; 
        upgradeCostText.text = upgradeCost.ToString();
    }

    public override void ExecuteClick()
    {
        var maxHp = playerSystem.GetMaxHp();

        decimal currentHp = (decimal)maxHp;
        decimal upgradeHp = currentHp * (decimal)upgradeMultiple;

        BigInteger newHp = new(Math.Ceiling(upgradeHp));

        displayValue = newHp;
        playerSystem.SetMaxHp(newHp);
    }
}
