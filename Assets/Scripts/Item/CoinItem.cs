using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CoinItem : DropPossessItem
{
    [SerializeField] private string dropGoldText;
    private BigInteger gold;

    protected override int PossessIndex => 0;

    public override void DropItem()
    {
        DropPossess(gold);
        UIManager.Instance.DropPossessEvent(this);
    }
}
