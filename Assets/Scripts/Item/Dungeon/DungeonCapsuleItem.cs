using System.Numerics;

public class DungeonCapsuleItem : DropPossessItem
{
    protected override void InitPossess()
    {
        var possessText = UIManager.Instance.GetPossessText();
        PossessText = possessText[0];
    }

    public override void InitPossessSet()
    {
        dropItemName = "capsule";
        dropAmountText = "20";
        dropAmount = BigInteger.Parse(dropAmountText);
    }
}
