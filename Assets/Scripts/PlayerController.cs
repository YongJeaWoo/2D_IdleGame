using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 애니메이터 컴포넌트
    private Animator animator;

    // 탐지 거리
    private float detectionDistance = 6f;

    // 탐지할 레이어
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


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectEnemy();
    }
}

