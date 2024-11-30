public class SettingPanel : CommonPanel
{
    private readonly string exitPanel = $"Exit Panel";
    protected override void InitPanel()
    {
        base.InitPanel();
        exitButton.onClick.AddListener(ExitPanelOpen);
    }

    public void ExitPanelOpen()
    {
        PopupManager.Instance.InstantPopup(exitPanel);
    }
}
