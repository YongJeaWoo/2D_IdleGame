using System.Numerics;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SpeedDataCell : DataCell
{
    private readonly float upgradeValue = 1f;
    private readonly string speedString = $"�ӵ� ��ȭ";

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

        numericalText.text = $"��ȭ �� \n{newSpeed}";
        upgradeCostText.text = upgradeCost.ToString();
    }

    public override void ExecuteClick()
    {
        var speed = playerSystem.GetSpeed();

        if (speed > 5)
        {
            numericalText.text = $"�ִ� ���� ����";
            Button button = GetComponentInChildren<Button>();
            button.interactable = false;
            var costText = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            costText.text = $"�ִ� ����";
            return;
        }

        float newSpeedFloat = speed + upgradeMultiple;

        BigInteger newSpeed = new(Mathf.Ceil(newSpeedFloat));
        displayValue = newSpeed;
        playerSystem.SetSpeed((float)displayValue);
    }
}
