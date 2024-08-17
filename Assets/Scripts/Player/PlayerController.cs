using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �ִϸ����� ������Ʈ
    private Animator animator;

    // Ž�� �Ÿ�
    private float detectionDistance = 6f;

    // Ž���� ���̾�
    [SerializeField] private LayerMask enemyLayer;

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

    // TODO : ���� �� ��� Ÿ���� ���ߵ��� �߰� ���� �ʿ�
    private void DetectObject(bool isAttack)
    {
        animator.SetBool("isRun", !isAttack);
        animator.SetBool("isAttack", isAttack);
    }

    private void DrawRay()
    {
        Vector2 rayPos = new Vector2(transform.position.x, transform.position.y + 0.25f);

        Debug.DrawRay(rayPos, Vector2.right * detectionDistance, Color.red);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        DetectEnemy();
    }
}

