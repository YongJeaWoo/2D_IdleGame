using System.Numerics;
using UnityEngine;

public abstract class BaseHealth : MonoBehaviour
{
    [SerializeField] protected string maxHpString;
    protected BigInteger currentHp;
    protected Animator anim;
    protected BigInteger maxHp;

    protected string AnimatorHitText = "Hit";
    protected string AnimatorDeathText = "Death";

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        SetHp();
    }

    protected abstract void SetHp();

    public virtual void Hit(BigInteger attackPoint)
    {
        if (currentHp - attackPoint <= 0)
        {
            //anim.SetTrigger(AnimatorDeathText);
            Death();
        }
        else
        {
            //anim.SetTrigger(AnimatorHitText);
            currentHp -= attackPoint;
        }
    }

    //TODO : »ç¸ÁÃ³¸®
    protected abstract void Death();
}
