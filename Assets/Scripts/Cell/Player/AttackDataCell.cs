using System;
using System.Numerics;
using UnityEngine;

public class AttackDataCell : DataCell
{
    private readonly float upgradeValue = 1.15f;
    private readonly string attackString = $"공격력 강화";

    protected override void OnEnable()
    {
        base.OnEnable();
        UpdateDisplay();
    }

    public override void InitInfo()
    {
        infoNameText.text = attackString;
        upgradeMultiple = upgradeValue;
    }

    public override void UpdateDisplay()
    {
        displayValue = playerSystem.GetAttack();

        decimal currentAttack = (decimal)displayValue;
        decimal upgradeAttack = currentAttack * (decimal)upgradeMultiple;

        BigInteger newAttack = new (Math.Ceiling(upgradeAttack));

        numericalText.text = $"강화 시 \n{newAttack}";
        upgradeCostText.text = upgradeCost.ToString();
    }

    public override void ExecuteClick()
    {
        var attack = playerSystem.GetAttack();

        decimal currentAttack = (decimal)attack;
        decimal upgradeAttack = currentAttack * (decimal)upgradeMultiple;

        BigInteger newAttack = new (Math.Ceiling(upgradeAttack));

        displayValue = newAttack;
        playerSystem.SetAttack(newAttack);
    }
}
