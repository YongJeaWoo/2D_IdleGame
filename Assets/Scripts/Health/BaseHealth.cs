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

    }
}
