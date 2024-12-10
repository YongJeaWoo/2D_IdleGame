public class ItemButton : ClickButtonComponent
{
    private readonly string NotImplement = $"Not Implement Panel";

    public override void ClickButton()
    {
        PopupManager.Instance.InstantPopup(NotImplement);
        return;
        //base.ClickButton();
    }
}
