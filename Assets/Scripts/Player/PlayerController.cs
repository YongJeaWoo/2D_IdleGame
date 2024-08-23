using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 애니메이터 컴포넌트
    private Animator animator;

    [Range(0.4f, 2f)]
    [SerializeField] private float attackSpeed;

    // 탐지할 레이어
    [SerializeField] private LayerMask enemyLayer;

    // 탐지 거리
    private float detectionDistance = 6f;

    // 추후 BackgroundController 변경해야 할 필요가 있을 수 있음
    private BackgroundController bgController;

    private void Start()
    {
        GetComponents();
    }

    private void GetComponents()
    {
        animator = GetComponent<Animator>();
        bgController = FindAnyObjectByType<BackgroundController>();
    }

    private void DetectEnemy()
    {
        Vector2 rayPos = new Vector2(transform.position.x, transform.position.y + 0.25f);

        RaycastHit2D[] hits = Physics2D.RaycastAll(rayPos, Vector2.right, detectionDistance, enemyLayer);

        if (hits.Length > 0)
        {
            DetectObject(true);
        }
        else
        {
            DetectObject(false);
        }
    }

    private void DetectObject(bool isAttack)
    {
        if (isAttack)
        {
            animator.speed = attackSpeed;
        }

        animator.SetBool("isRun", !isAttack);
        animator.SetBool("isAttack", isAttack);

        bgController.BG_Controll(isAttack);
    }
    
    void Update()
    {
        DetectEnemy();
    }
}