using System.Numerics;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    [SerializeField] protected string maxHpString;
    protected BigInteger currentHp;
    protected Animator anim;
    protected BigInteger maxHp;

    protected ObjectPool pool;

    protected virtual void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        anim = GetComponent<Animator>();
        SetHp();
    }

    public virtual void Hit(BigInteger attackPoint)
    {
        if (currentHp - attackPoint <= 0)
        {
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
        if (pool == null)
        {
            pool = ObjectPoolManager.Instance.FindObjectPool(gameObject);
        }

        pool.ReleasePoolObject(gameObject);
    }
}
