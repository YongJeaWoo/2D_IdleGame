using System.Collections;
using System.Numerics;
using UnityEngine;

public class OverDriveAttackPower : CoolTimeDisplay
{
    [Header("이팩트 사운드")]
    [SerializeField] private AudioClip powerUpEffectSound;

    [SerializeField] private GameObject effectPrefab;

    private readonly float arrangeTime = 10f;

    private ExplainableComponent explain;

    protected override void Start()
    {
        base.Start();
        ObjectPoolManager.Instance.InitObjectPool(effectPrefab);
        explain = GetComponent<ExplainableComponent>();
        explain.SetExplainDetail($"{arrangeTime} 초 동안 플레이어의 \n자체 공격력이 2배로 증가합니다.");
    }

    private IEnumerator TemporaryAttackUpCoroutine(PlayerManager playerManager, BigInteger playerAttack)
    {
        if (PlayerManager.Instance.GetPlayer().GetComponent<PlayerHealth>().GetIsPlayerDead())
        {
            StopAllCoroutines();
            AudioManager.Instance.StopSFX(powerUpEffectSound);
            yield break;
        }

        var player = playerManager.GetPlayer();
        var obj = ObjectPoolManager.Instance.GetToPool(effectPrefab);
        var yPosModify = player.transform.position + new UnityEngine.Vector3(0, 0.3f, 0);
        obj.transform.SetPositionAndRotation(yPosModify, UnityEngine.Quaternion.identity);
        var originAttack = playerAttack;
        var tempAttack = playerAttack * 2;
        playerManager.SetAttack(tempAttack);

        yield return new WaitForSeconds(arrangeTime);
        ObjectPoolManager.Instance.ReleaseToPool(obj);
        playerManager.SetAttack(originAttack);
    }

    public override void PerformingAction()
    {
        var attack = PlayerManager.Instance.GetAttack();
        StartCoroutine(TemporaryAttackUpCoroutine(PlayerManager.Instance, attack));
        EffectSound();
    }

    private void EffectSound()
    {
        AudioManager.Instance.PlaySFX(powerUpEffectSound);
    }
}
