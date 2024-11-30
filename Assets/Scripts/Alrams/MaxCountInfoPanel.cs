public class MaxCountInfoPanel : AlramPanel
{
    protected override void Start()
    {
        base.Start();
        infoText.text = $"제작 가능한 최대치에 도달했습니다.";
    }
}
