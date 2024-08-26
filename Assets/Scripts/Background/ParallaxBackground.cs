using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Header("이어질 배경")]
    [SerializeField] private Transform targetBG;
    [Header("이어질 두 배경 사이 거리")]
    [SerializeField] private float scrollAmount;
    [Header("이동 방향")]
    [SerializeField] private Vector3 moveDirection;
    [Header("스킬 영향을 받는 여부 Enum값")]
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
