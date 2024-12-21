using UnityEngine;

public class PoisonAttack : CoolTimeDisplay
{
    [Header("던져지는 스킬 구체 프리팹")]
    [SerializeField] private GameObject poisonBallPrefab;
    [Header("던지는 힘")]
    [SerializeField] private float launchForce = 10f;
    [Header("던지는 방향")]
    [SerializeField] private Vector2 launchAngle = new(1, 1);
    private readonly float arrangeTime = 10f;
    private ExplainableComponent explain;

    protected override void Start()
    {
        base.Start();
        ObjectPoolManager.Instance.InitObjectPool(poisonBallPrefab);
        explain = GetComponent<ExplainableComponent>();
        explain.SetExplainDetail($"플레이어 앞 방향으로 스킬을 날려 \n 범위 안의 적들을 {arrangeTime} 초 동안 대미지를 입힙니다."); 
    }

    private void LaunchSkill()
    {
        var playerManager = PlayerManager.Instance;
        if (playerManager == null || poisonBallPrefab == null) return;

        Vector3 spawnPosition = playerManager.GetPlayer().transform.position;
        spawnPosition.y += 0.2f;

        GameObject skill = ObjectPoolManager.Instance.GetToPool(poisonBallPrefab);

        skill.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);

        Rigidbody2D rb = skill.GetComponent<Rigidbody2D>();

        Vector2 direction = launchAngle.normalized;
        rb.AddForce(direction * launchForce, ForceMode2D.Impulse);
    }

    public override void PerformingAction()
    {
        LaunchSkill();
    }
}
