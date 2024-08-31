using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStatCell : Cell
{
    protected override void Start()
    {
        base.Start();
        statIncreaseAmount = playerSystem.GetAttack();
        statIncreaseAmountText.text = statIncreaseAmount.ToString();
    }

    public override void OnButtonClick()
    {
        var possessionsController = playerSystem.GetPossessionsController();

        if (!possessionsController.SpendGold(cost)) return;

        var atk = playerSystem.GetAttack();

        atk += 10;

        playerSystem.SetAttack(atk);

        statIncreaseAmount = atk;

        base.OnButtonClick();

    }
}
