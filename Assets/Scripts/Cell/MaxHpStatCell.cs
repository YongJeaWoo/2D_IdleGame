using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHpStatCell : Cell
{
    protected override void Start()
    {
        base.Start();
        statIncreaseAmount = playerSystem.GetMaxHp();
        statIncreaseAmountText.text = statIncreaseAmount.ToString();
    }

    public override void OnButtonClick()
    {
        var possessionsController = playerSystem.GetPossessionsController();

        if (!possessionsController.SpendGold(cost)) return;

        var maxHp = playerSystem.GetMaxHp();

        maxHp += 1000;

        playerSystem.SetMaxHp(maxHp);

        statIncreaseAmount = maxHp;
        base.OnButtonClick();
    }
}
