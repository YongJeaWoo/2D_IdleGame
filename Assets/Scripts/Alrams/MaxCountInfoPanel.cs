public class MaxCountInfoPanel : AlramPanel
{
    private readonly string MaxCount = $"제작 가능한 최대치에 도달했습니다.";

    protected override void Start()
    {
        base.Start();
        infoText.text = MaxCount;
    }
}
