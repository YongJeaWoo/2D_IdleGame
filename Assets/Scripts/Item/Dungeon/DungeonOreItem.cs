using System.Numerics;

public class DungeonOreItem : DropPossessItem
{
    protected override void InitPossess()
    {
        var possessText = UIManager.Instance.GetPossessText();
        PossessText = possessText[1];
    }

    public override void InitPossessSet()
    {
        dropItemName = $"ore";
        dropAmountText = $"200";
        dropAmount = BigInteger.Parse(dropAmountText);
    }
}
