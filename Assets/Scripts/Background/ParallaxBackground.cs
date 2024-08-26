using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Header("�̾��� ���")]
    [SerializeField] private Transform targetBG;
    [Header("�̾��� �� ��� ���� �Ÿ�")]
    [SerializeField] private float scrollAmount;
    [Header("�̵� ����")]
    [SerializeField] private Vector3 moveDirection;
    [Header("��ų ������ �޴� ���� Enum��")]
    [SerializeField] private BG_OPTION option;

    private float moveSpeed;
    private float originSpeed;

    private PlayerSystem playerSystem;

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
        BackgroundSetSpeed();
        originSpeed = moveSpeed;
    }

    private void BackgroundSetSpeed()
    {
        playerSystem = FindObjectOfType<PlayerSystem>();
        var player = playerSystem.GetPlayer();
        var speed = player.GetComponent<SpeedComponent>();

        UpdateSpeed(speed);
    }

    public void UpdateSpeed(SpeedComponent _speed)
    {
        float baseSpeed = _speed.GetSpeed();
        int objectIndex = int.Parse(gameObject.name.Substring(gameObject.name.Length - 1)) - 1;

        moveSpeed = baseSpeed - (objectIndex * 0.2f);
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
