using System.Numerics;
using UnityEngine.EventSystems;

public class HealingPlayer : CoolTimeDisplay
{
    private PlayerSystem playerSystem;

    private readonly BigInteger healAmount = 20;

    protected override void Start()
    {
        base.Start();
        playerSystem = FindObjectOfType<PlayerSystem>();
        explainDetail = $"플레이어에게 {healAmount}만큼 \n체력을 회복시킵니다.";
    }

    public override void BehaviourButtonClick()
    {
        if (isCoolTime) return;

        isCoolTime = true;
        playerSystem.SetCurrentHp(healAmount);
        StartCoroutine(CoolTime());
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        explainText.text = explainDetail;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        explainText.text = string.Empty;
    }
}
