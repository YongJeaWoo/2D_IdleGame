using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Header("�̾��� ���")]
    [SerializeField] private Transform targetBG;
    [Header("�̾��� �� ��� ���� �Ÿ�")]
    [SerializeField] private float scrollAmount;
    [Header("��� ������ �ӵ�")]
    [SerializeField] private float moveSpeed;
    private float originSpeed;
    [Header("�̵� ����")]
    [SerializeField] private Vector3 moveDirection;
    [Header("��ų ������ �޴� ���� Enum��")]
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
