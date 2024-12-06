using System.Collections;
using UnityEngine;

public class Flamethrower : CoolTimeDisplay
{
    [Header("화염 방사기 프리팹")]
    [SerializeField] private GameObject flamePrefab;
    [Header("방향")]
    [SerializeField] private Vector2 launchAngle = new(1, 0);

    private PlayerSystem playerSystem;
    private readonly float arrangeTime = 5f;

    protected override void Start()
    {
        base.Start();
        playerSystem = FindObjectOfType<PlayerSystem>();
        ObjectPoolManager.Instance.InitObjectPool(flamePrefab);
        explainDetail = $"앞 방향으로 {arrangeTime} 초 동안 \n불을 뿜습니다.";
    }

    public override void BehaviourButtonClick()
    {
        if (isCoolTime) return;

        isCoolTime = true;
        StartCoroutine(CoolTime());
        StartCoroutine(FlameThrowerCoroutine());
    }

    private IEnumerator FlameThrowerCoroutine()
    {
        var player = playerSystem.GetPlayer();
        var attackComponent = player.GetComponent<PlayerAttack>();
        var pos = attackComponent.GetAttackPos();

        GameObject skill = ObjectPoolManager.Instance.GetToPool(flamePrefab);

        skill.transform.position = pos.transform.position;

        yield return null;
    }
}
