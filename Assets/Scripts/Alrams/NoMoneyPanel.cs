public class NoMoneyPanel : AlramPanel
{
    private readonly string noMoney = $"가지고 있는 자원이 부족합니다.";

    protected override void Start()
    {
        base.Start();
        infoText.text = noMoney;
    }
}
