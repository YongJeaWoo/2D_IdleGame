public class ShopButton : ClickButtonComponent
{
    private readonly string NotImplement = $"Warning Panel";
    private readonly string NotImplementExplainText = $"이 기능은 현재 구현 중입니다.";

    public override void ClickButton()
    {
        var panel = PopupManager.Instance.InstantPopup(NotImplement);
        var warningPanel = panel.GetComponent<WarningPanel>();
        warningPanel.SetAlramPanelText(NotImplementExplainText);
        return;
    }
}
