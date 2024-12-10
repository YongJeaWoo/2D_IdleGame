using System.Numerics;

public class HealingPlayer : CoolTimeDisplay
{
    private PlayerSystem playerSystem;
    private ExplainableComponent explain;

    private readonly BigInteger healAmount = 20;

    protected override void Start()
    {
        base.Start();
        playerSystem = FindObjectOfType<PlayerSystem>();
        explain = GetComponent<ExplainableComponent>();
        explain.SetExplainDetail($"플레이어에게 {healAmount}만큼 \n체력을 회복시킵니다.");
    }

    public override void BehaviourButtonClick()
    {
        base.BehaviourButtonClick();
        playerSystem.SetCurrentHp(healAmount);
    }
}
