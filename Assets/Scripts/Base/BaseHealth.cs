using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TakeDamageTextComponent))]
public class BaseHealth : MonoBehaviour
{
    [Header("피격 시 이팩트 사운드")]
    [SerializeField] protected AudioClip hitSound;
    [Header("체력 최댓값")]
    [SerializeField] protected string maxHpString;
    [Header("피격 시 이팩트")]
    [SerializeField] protected GameObject hitEffectPrefab;

    protected TakeDamageTextComponent takeDamage;
    protected Animator anim;
    protected Renderer myRender;
    protected BigInteger currentHp;
    protected BigInteger maxHp;

    protected Image myHealthBar;
    protected TextMeshProUGUI myHealthText;

    protected virtual void Awake()
    {
        GetComponents();
    }

    protected virtual void Start()
    {
        HitPoolObject();
    }

    protected virtual void GetComponents()
    {
        anim = GetComponent<Animator>();
        takeDamage = GetComponent<TakeDamageTextComponent>();
        myRender = GetComponent<Renderer>();
    }

    protected virtual void HitPoolObject()
    {
        ObjectPoolManager.Instance.InitObjectPool(hitEffectPrefab);
    }

    protected virtual void SetValues()
    {
        myHealthBar = UIManager.Instance.GetHpBars()[1];
        myHealthText = UIManager.Instance.GetHpTexts()[1];
    }

    public virtual void Hit(BigInteger attackPoint)
    {
        AudioManager.Instance.PlaySFX(hitSound);
        currentHp -= attackPoint;
        takeDamage.ShowDamagedText(attackPoint);
        var hitEffect = ObjectPoolManager.Instance.GetToPool(hitEffectPrefab);
        UnityEngine.Vector3 effectPosition = transform.position;
        effectPosition.y += 0.5f;
        hitEffect.transform.position = effectPosition;

        if (currentHp <= 0)
        {
            currentHp = 0;
            Death();
        }
    }

    protected virtual void SetCurrentHpToMaxHp()
    {
        currentHp = maxHp;
    }

    protected virtual void Death()
    {
        ObjectPoolManager.Instance.ReleaseToPool(gameObject);
    }

    public Image GetHealthImage() => myHealthBar;
    public TextMeshProUGUI GetHealthText() => myHealthText;
    public BigInteger GetCurrentHp() => currentHp;
    public BigInteger SetCurrentHp(BigInteger value)
    {
        if (currentHp >= maxHp) return currentHp;
        return currentHp = value;
    }
    public BigInteger GetMaxHp() => maxHp;
}
