using System.Numerics;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    [SerializeField] protected string maxHpString;
    protected BigInteger currentHp;
    protected Animator anim;
    protected BigInteger maxHp;
    protected DamageTextPopup textPopup;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        SetHp();
    }

    public virtual void Hit(BigInteger attackPoint)
    {
        currentHp -= attackPoint;

        if(textPopup == null)
        {
            textPopup = FindAnyObjectByType<DamageTextPopup>();
        }
        textPopup.ShowDamageText(attackPoint, transform.position);

        if (currentHp - attackPoint <= 0)
        {
            currentHp = 0;
            Death();
            return;
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
