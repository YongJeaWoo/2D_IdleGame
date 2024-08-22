using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class EnemyHealth : BaseHealth
{
    [SerializeField] private GameObject deathParticle;
    protected override void SetHp()
    {
        maxHp = BigInteger.Parse(maxHpString);
        currentHp = maxHp;
    }

    protected override void Death()
    {
        //Instantiate(deathParticle, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
