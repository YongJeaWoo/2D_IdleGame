using System.Collections;
using System.Numerics;
using UnityEngine;

public class OverDriveAttackPower : CoolTimeDisplay
{
    private readonly float arrangeTime = 10f;

    private PlayerSystem playerSystem;
    private ExplainableComponent explain;

    protected override void Start()
    {
        base.Start();
        playerSystem = FindObjectOfType<PlayerSystem>();
        explain = GetComponent<ExplainableComponent>();
        explain.SetExplainDetail($"{arrangeTime} 초 동안 플레이어의 \n자체 공격력이 2배로 증가합니다.");
    }

    public override void BehaviourButtonClick()
    {
        base.BehaviourButtonClick();
        var attack = playerSystem.GetAttack();
        StartCoroutine(TemporaryAttackUpCoroutine(playerSystem, attack));
    }

    private IEnumerator TemporaryAttackUpCoroutine(PlayerSystem playerSystem, BigInteger playerAttack)
    {
        var originAttack = playerAttack;
        var tempAttack = playerAttack * 2;
        playerSystem.SetAttack(tempAttack);

        yield return new WaitForSeconds(arrangeTime);

        playerSystem.SetAttack(originAttack);
    }
}
