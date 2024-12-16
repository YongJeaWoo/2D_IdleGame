using System.Numerics;
using UnityEngine;

public class HealingPlayer : CoolTimeDisplay
{
    [Header("이팩트 사운드")]
    [SerializeField] private AudioClip healingEffectSound;

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

    private void EffectSound()
    {
        AudioManager.Instance.PlaySFX(healingEffectSound);
    }

    public override void PerformingAction()
    {
        playerSystem.SetCurrentHp(healAmount);
        EffectSound();
    }
}
