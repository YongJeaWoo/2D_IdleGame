using System.Numerics;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SpeedDataCell : DataCell
{
    private readonly float upgradeValue = 1f;
    private readonly string speedString = $"속도 강화";

    protected override void OnEnable()
    {
        base.OnEnable();
        UpdateDisplay();
    }

    public override void InitInfo()
    {
        infoNameText.text = speedString;
        upgradeMultiple = upgradeValue;
    }

    public override void UpdateDisplay()
    {
        float speed = playerSystem.GetSpeed();

        decimal currentSpeed = (decimal)speed;
        decimal upgradeSpeed = currentSpeed * (decimal)upgradeMultiple;

        BigInteger newSpeed = new(Math.Ceiling(upgradeSpeed));

        numericalText.text = $"강화 시 \n{newSpeed}";
        upgradeCostText.text = upgradeCost.ToString();
    }

    public override void ExecuteClick()
    {
        var speed = playerSystem.GetSpeed();

        if (speed > 5)
        {
            numericalText.text = $"최대 레벨 도달";
            Button button = GetComponentInChildren<Button>();
            button.interactable = false;
            var costText = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            costText.text = $"최대 레벨";
            return;
        }

        float newSpeedFloat = speed + upgradeMultiple;

        BigInteger newSpeed = new(Mathf.Ceil(newSpeedFloat));
        displayValue = newSpeed;
        playerSystem.SetSpeed((float)displayValue);
    }
}
