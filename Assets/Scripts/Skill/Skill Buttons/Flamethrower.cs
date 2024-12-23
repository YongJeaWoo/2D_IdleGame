using UnityEngine;

public class Flamethrower : CoolTimeDisplay
{
    [Header("화염 방사기 프리팹")]
    [SerializeField] private GameObject flamePrefab;

    private ExplainableComponent explain;
    
    private readonly float arrangeTime = 5f;

    protected override void Start()
    {
        base.Start();
        ObjectPoolManager.Instance.InitObjectPool(flamePrefab);
        explain = GetComponent<ExplainableComponent>();
        explain.SetExplainDetail($"앞 방향으로 {arrangeTime} 초 동안 \n불을 뿜습니다.");
    }

    private void FlameThrowerEffect()
    {
        var player = PlayerManager.Instance.GetPlayer();
        var attackComponent = player.GetComponent<PlayerAttack>();
        var pos = attackComponent.GetAttackPos();

        GameObject skill = ObjectPoolManager.Instance.GetToPool(flamePrefab);

        skill.transform.position = pos.transform.position;
    }

    public override void PerformingAction()
    {
        FlameThrowerEffect();
    }
}
