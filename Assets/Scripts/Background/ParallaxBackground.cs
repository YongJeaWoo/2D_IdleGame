using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Header("이어질 배경")]
    [SerializeField] private Transform targetBG;
    [Header("이어질 두 배경 사이 거리")]
    [SerializeField] private float scrollAmount;
    [Header("배경 움직임 속도")]
    [SerializeField] private float moveSpeed;
    private float originSpeed;
    [Header("이동 방향")]
    [SerializeField] private Vector3 moveDirection;
    [Header("스킬 영향을 받는 여부 Enum값")]
    [SerializeField] private BG_OPTION option;

    private void Start()
    {
        InitValue();
    }

    private void Update()
    {
        MoveBackground();
    }

    private void InitValue()
    {
        originSpeed = moveSpeed;
    }

    private void MoveBackground()
    {
        transform.position += moveSpeed * Time.deltaTime * moveDirection;

        if (transform.position.x <= -scrollAmount)
        {
            transform.position = targetBG.position - moveDirection * scrollAmount;
        }
    }

    public float PlayerAttackToSpeed(bool isAttack)
    {
        if (isAttack)
        {
            moveSpeed = 0;
            return moveSpeed;
        }
        else
        {
            moveSpeed = originSpeed;
            return moveSpeed;
        }
    }

    public BG_OPTION GetOption() => option;
}
