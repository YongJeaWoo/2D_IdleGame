using System.Collections;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;

public class OverDriveAttackPower : CoolTimeDisplay
{
    private readonly float arrangeTime = 10f;

    private PlayerSystem playerSystem;

    protected override void Start()
    {
        base.Start();
        playerSystem = FindObjectOfType<PlayerSystem>();
        explainDetail = $"{arrangeTime} 초 동안 플레이어의 \n자체 공격력이 2배로 증가합니다.";
    }

    public override void BehaviourButtonClick()
    {
        if (isCoolTime || isExplainActive) return;

        isCoolTime = true;
        var attack = playerSystem.GetAttack();
        StartCoroutine(CoolTime());
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

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        
        explainText.text = explainDetail;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        explainText.text = string.Empty;

        base.OnPointerUp(eventData);
    }
}
