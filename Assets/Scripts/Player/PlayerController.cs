using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �ִϸ����� ������Ʈ
    private Animator animator;
    private SpeedComponent speed;

    // Ž���� ���̾�
    [SerializeField] private LayerMask enemyLayer;

    // Ž�� �Ÿ�
    private float detectionDistance = 6f;

    // ���� BackgroundController �����ؾ� �� �ʿ䰡 ���� �� ����
    private BackgroundController bgController;

    private void Start()
    {
        GetComponents();
    }

    private void GetComponents()
    {
        animator = GetComponent<Animator>();
        speed = GetComponent<SpeedComponent>();
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
        var m_speed = speed.GetSpeed();
        animator.speed = m_speed;

        animator.SetBool("isRun", !isAttack);
        animator.SetBool("isAttack", isAttack);

        bgController.BG_Controll(isAttack);
    }

    void Update()
    {
        DetectEnemy();
    }
}