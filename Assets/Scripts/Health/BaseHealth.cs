using System.Numerics;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    [Header("데미지 텍스트 프리팹")]
    [SerializeField] private GameObject damageTextPrefab;

    [Header("최대 HP")]
    [SerializeField] protected string maxHpString;

    private float plusValue = 0.6f;
    private Transform canvasTransform;

    protected BigInteger currentHp;
    protected Animator anim;
    protected BigInteger maxHp;

    protected virtual void Start()
    {
        GetComponents();
        ObjectPoolManager.Instance.InitObjectPool(damageTextPrefab);
        canvasTransform = UIManager.Instance.GetCanvas().transform;
    }

    protected virtual void GetComponents()
    {
        anim = GetComponent<Animator>();
    }

    public virtual void Hit(BigInteger attackPoint)
    {
        currentHp -= attackPoint;

        ShowDamageText(attackPoint, transform.position);

        if (currentHp - attackPoint <= 0)
        {
            currentHp = 0;
            Death();
            return;
        }
    }

    protected void ShowDamageText(BigInteger damage, UnityEngine.Vector3 position)
    {
        position.y += plusValue;

        GameObject damageText = ObjectPoolManager.Instance.GetToPool(damageTextPrefab, canvasTransform);
        damageText.transform.SetParent(canvasTransform);

        UnityEngine.Vector2 screenPosition = Camera.main.WorldToScreenPoint(position);
        damageText.transform.position = screenPosition;

        var text = damageText.GetComponent<DamageText>();
        text.ShowText(damage);
    }

    protected virtual void SetHp()
    {
        maxHp = BigInteger.Parse(maxHpString);
        currentHp = maxHp;
    }

    //TODO : 사망처리
    protected virtual void Death()
    {
        ObjectPoolManager.Instance.ReleaseToPool(gameObject);
    }

    public BigInteger GatCurrentHp() => currentHp;
    public BigInteger GetMaxHp() => maxHp;
}
