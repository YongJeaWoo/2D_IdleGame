using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class OreItem : DropPossessItem
{
    [SerializeField] private string dropOreText;
    private BigInteger ore;

    protected override int PossessIndex => 1; // 0번 인덱스는 골드로 가정

    public override void DropItem()
    {
        DropPossess(ore);
        UIManager.Instance.DropPossessEvent(this);
    }
}
