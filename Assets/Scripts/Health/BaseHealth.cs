using System.Numerics;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    [SerializeField] protected string maxHpString;
    protected BigInteger currentHp;
    protected Animator anim;
    protected BigInteger maxHp;

    protected virtual void Start()
    {
        GetComponents();
    }

    protected virtual void GetComponents()
    {
        anim = GetComponent<Animator>();
    }

    public virtual void Hit(BigInteger attackPoint)
    {
        if (currentHp - attackPoint <= 0)
        {
            currentHp = 0;

            Death();
        }
        else
        {
            currentHp -= attackPoint;
        }
    }

    protected virtual void SetHp()
    {
        maxHp = BigInteger.Parse(maxHpString);
        currentHp = maxHp;
    }

    //TODO : »ç¸ÁÃ³¸®
    protected virtual void Death()
    {
        ObjectPoolManager.Instance.ReleaseToPool(gameObject);
    }

    public BigInteger GatCurrentHp() => currentHp;
    public BigInteger GetMaxHp() => maxHp;
}
