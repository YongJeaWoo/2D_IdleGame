using System.Collections;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TakeDamageTextComponent))]
public class BaseHealth : MonoBehaviour
{
    [Header("Ã¼·Â ÃÖ´ñ°ª")]
    [SerializeField] protected string maxHpString;
    [Header("ÇÇ°Ý ½Ã ÀÌÆÑÆ®")]
    [SerializeField] protected GameObject hitEffectPrefab;

    protected TakeDamageTextComponent takeDamage;
    protected Animator anim;
    protected Renderer myRender;
    protected BigInteger currentHp;
    protected BigInteger maxHp;

    protected Image myHealthBar;
    protected TextMeshProUGUI myHealthText;

    protected virtual void Start()
    {
        GetComponents();
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

    public virtual void Hit(BigInteger attackPoint)
    {
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

    //TODO : »ç¸ÁÃ³¸®
    protected virtual void Death()
    {
        ObjectPoolManager.Instance.ReleaseToPool(gameObject);
    }

    public Image GetHealthImage() => myHealthBar;
    public TextMeshProUGUI GetHealthText() => myHealthText;
    public BigInteger GetCurrentHp() => currentHp;
    public BigInteger GetMaxHp() => maxHp;
}
