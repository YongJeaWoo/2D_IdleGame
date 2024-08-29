using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class OreItem : DropPossessItem
{
    [SerializeField] private string dropOreText;
    private BigInteger ore;

    protected void OnEnable()
    {
        ore = BigInteger.Parse(dropOreText);
    }

    public override void DropItem()
    {
        var hasOre = possessionsController.GetHasOre();

        hasOre += ore;

        UIManager.Instance.UpdatePossessText(PossessText, hasOre);

        possessionsController.SetHasOre(hasOre);
    }

    protected override void InitPossess()
    {
        var possessText = UIManager.Instance.GetPossessText();
        PossessText = possessText[1];
    }


}
