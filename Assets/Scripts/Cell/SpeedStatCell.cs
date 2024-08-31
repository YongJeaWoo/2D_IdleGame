using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class SpeedStatCell : Cell
{
    protected override void Start()
    {
        base.Start();
        var increaseValue = Mathf.Ceil(playerSystem.GetSpeed());
        statIncreaseAmount = (BigInteger)increaseValue;
        statIncreaseAmountText.text = statIncreaseAmount.ToString();
    }

    public override void OnButtonClick()
    {
        var possessionsController = playerSystem.GetPossessionsController();

        if (!possessionsController.SpendGold(cost)) return;

        var currentSpeed = playerSystem.GetSpeed();

        float newSpeedMultiplier = 1.1f;

        playerSystem.SetSpeed(newSpeedMultiplier);

        statIncreaseAmount = (BigInteger)Mathf.Ceil(currentSpeed * newSpeedMultiplier);

        base.OnButtonClick();
    }
}
