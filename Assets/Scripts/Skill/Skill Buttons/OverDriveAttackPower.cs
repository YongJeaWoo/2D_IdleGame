using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class OverDriveAttackPower : CoolTimeDisplay
{
    [Header("이팩트 사운드")]
    [SerializeField] private AudioClip powerUpEffectSound;

    [SerializeField] private GameObject effectPrefab;

    private readonly float arrangeTime = 10f;

    private PlayerSystem playerSystem;
    private ExplainableComponent explain;

    protected override void Start()
    {
        base.Start();
        ObjectPoolManager.Instance.InitObjectPool(effectPrefab);
        playerSystem = FindObjectOfType<PlayerSystem>();
        explain = GetComponent<ExplainableComponent>();
        explain.SetExplainDetail($"{arrangeTime} 초 동안 플레이어의 \n자체 공격력이 2배로 증가합니다.");
    }

    private IEnumerator TemporaryAttackUpCoroutine(PlayerSystem playerSystem, BigInteger playerAttack)
    {
        var player = playerSystem.GetPlayer();
        var obj = ObjectPoolManager.Instance.GetToPool(effectPrefab);
        var yPosModify = player.transform.position + new UnityEngine.Vector3(0, 0.3f, 0);
        obj.transform.SetPositionAndRotation(yPosModify, UnityEngine.Quaternion.identity);
        var originAttack = playerAttack;
        var tempAttack = playerAttack * 2;
        playerSystem.SetAttack(tempAttack);

        yield return new WaitForSeconds(arrangeTime);
        ObjectPoolManager.Instance.ReleaseToPool(obj);
        playerSystem.SetAttack(originAttack);
    }

    public override void PerformingAction()
    {
        var attack = playerSystem.GetAttack();
        StartCoroutine(TemporaryAttackUpCoroutine(playerSystem, attack));
        EffectSound();
    }

    private void EffectSound()
    {
        AudioManager.Instance.PlaySFX(powerUpEffectSound);
    }
}
