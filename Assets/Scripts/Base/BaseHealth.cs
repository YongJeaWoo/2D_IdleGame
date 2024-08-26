using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TakeDamageTextComponent))]
public class BaseHealth : MonoBehaviour
{
    [Header("Ã¼·Â ÃÖ´ñ°ª")]
    [SerializeField] protected string maxHpString;

    protected TakeDamageTextComponent takeDamage;
    protected BigInteger currentHp;
    protected Animator anim;
    protected BigInteger maxHp;

    protected Image myHealthBar;
    protected TextMeshProUGUI myHealthText;

    protected virtual void Start()
    {
        GetComponents();
    }

    protected virtual void GetComponents()
    {
        anim = GetComponent<Animator>();
        takeDamage = GetComponent<TakeDamageTextComponent>();
    }

    public virtual void Hit(BigInteger attackPoint)
    {
        currentHp -= attackPoint;
        takeDamage.ShowDamagedText(attackPoint);

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
