public class NotImplementPanel : AlramPanel
{
    private readonly string notImplement = $"현재 기능을 구현 중입니다.";

    protected override void Start()
    {
        base.Start();
        infoText.text = notImplement;
    }
}
