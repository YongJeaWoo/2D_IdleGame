using System.Numerics;

public class OreItem : DropPossessItem
{
    protected override void InitPossess()
    {
        var possessText = UIManager.Instance.GetPossessText();
        PossessText = possessText[1];
    }

    public override void InitPossessSet()
    {
        dropAmount = BigInteger.Parse(dropAmountText);
    }
}
