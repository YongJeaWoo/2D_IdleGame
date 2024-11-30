public class ExitPanel : CommonPanel
{
    protected override void InitPanel()
    {
        base.InitPanel();
        exitButton.onClick.AddListener(ExitGame);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                            Application.Quit();
        #endif
    }
}
