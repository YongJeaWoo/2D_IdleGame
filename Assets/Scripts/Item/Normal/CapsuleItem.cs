using System.Numerics;

public class CapsuleItem : DropPossessItem
{
    protected override void InitPossess()
    {
        var possessText = UIManager.Instance.GetPossessText();
        PossessText = possessText[2];
    }

    public override void InitPossessSet()
    {
        dropAmount = BigInteger.Parse(dropAmountText);
    }
}
