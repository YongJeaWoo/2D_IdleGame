using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class CoinItem : DropPossessItem
{
    [SerializeField] private string dropGoldText;
    private BigInteger gold;

    protected void OnEnable()
    {
        gold = BigInteger.Parse(dropGoldText);
    }

    public override void DropItem()
    {
        var hasGold = possessionsController.GetHasGold();

        hasGold += gold;

        UIManager.Instance.UpdatePossessText(PossessText, hasGold);

        possessionsController.SetHasGold(hasGold);
    }

    protected override void InitPossess()
    {
        var possessText = UIManager.Instance.GetPossessText();
        PossessText = possessText[0];
    }
}
