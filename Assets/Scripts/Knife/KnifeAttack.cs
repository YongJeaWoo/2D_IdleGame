using System.Numerics;
using UnityEngine;

public class KnifeAttack : MonoBehaviour
{
    [SerializeField] private string attackPointString;
    private BigInteger attackPoint;
    private BigInteger finalAttackPoint;
    private BigInteger playerAttack;

    private void Start()
    {
        InitializePlayerManager();
    }

    private void OnEnable()
    {
        InitValues();
    }

    private void InitializePlayerManager()
    {
        var playerManager = PlayerManager.Instance;
        if (playerManager == null)
        {
            Debug.LogError("PlayerManager를 찾을 수 없음");
            return;
        }

        if (playerManager.GetPlayer() != null)
        {
            UpdateAttackPoints();
        }
        else
        {
            playerManager.OnPlayerReady += OnPlayerReadyCallback;
        }
    }

    private void OnPlayerReadyCallback()
    {
        PlayerManager.Instance.OnPlayerReady -= OnPlayerReadyCallback;
        UpdateAttackPoints();
    }

    private void InitValues()
    {
        attackPoint = BigInteger.Parse(attackPointString);
        if (PlayerManager.Instance != null)
        {
            UpdateAttackPoints();
        }
    }

    private void UpdateAttackPoints()
    {
        if (PlayerManager.Instance.GetPlayer() == null) return;

        playerAttack = PlayerManager.Instance.GetAttack();
        finalAttackPoint = attackPoint + playerAttack;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BaseHealth targetHealth = collision.GetComponent<BaseHealth>();

        if (targetHealth != null)
        {
            if (collision.CompareTag("Enemy"))
            {
                TargetHit(targetHealth, finalAttackPoint);
            }
            else if (collision.CompareTag("Dungeon Object"))
            {
                TargetHit(targetHealth, 1);
            }
        }
    }

    private void TargetHit(BaseHealth targetHealth, BigInteger value)
    {
        targetHealth.Hit(value);
        Release();
    }

    private void Release()
    {
        ObjectPoolManager.Instance.ReleaseToPool(gameObject);
    }

    public string GetAttackPointString() => attackPointString;
    public BigInteger GetAttackPoint() => attackPoint;
}
