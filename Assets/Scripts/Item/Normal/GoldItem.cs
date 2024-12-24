using System.Numerics;

public class GoldItem : DropPossessItem
{
    protected override void InitPossess()
    {
        var possessText = UIManager.Instance.GetPossessText();
        PossessText = possessText[0];
    }

    public override void InitPossessSet()
    {
        dropAmount = BigInteger.Parse(dropAmountText);
    }
}
