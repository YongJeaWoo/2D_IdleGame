using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �ִϸ����� ������Ʈ
    private Animator animator;

    // Ž�� �Ÿ�
    private float detectionDistance = 6f;

    // Ž���� ���̾�
    [SerializeField] private LayerMask enemyLayer;

    // ���� BackgroundController �����ؾ� �� �ʿ䰡 ���� �� ����
    private BackgroundController bgController;

    private void Start()
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
        animator.SetBool("isRun", !isAttack);
        animator.SetBool("isAttack", isAttack);

        bgController.BG_Controll(isAttack);
    }
    
    void Update()
    {
        DetectEnemy();
    }
}