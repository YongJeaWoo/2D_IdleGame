using System.Numerics;

public class DungeonGoldItem : DropPossessItem
{
    protected override void InitPossess()
    {
        var possessText = UIManager.Instance.GetPossessText();
        PossessText = possessText[0];
    }

    public override void InitPossessSet()
    {
        dropItemName = "gold";
        dropAmountText = $"200";
        dropAmount = BigInteger.Parse(dropAmountText);
    }
}
