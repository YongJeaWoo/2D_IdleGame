using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : BaseAttack
{
    [SerializeField] private Transform attackPos;

    [SerializeField] private GameObject weapon;
    
    private ObjectPool pool;

    private void Start()
    {
        ObjectPoolManager.Instance.InitObjectPool(weapon, ref pool);
    }

    public override void AttackAnimation()
    {
        var objPool = ObjectPoolManager.Instance.FindObjectPool(weapon);
        var knifeObj = objPool.GetPoolObject();

        knifeObj.transform.SetParent(objPool.gameObject.transform);

        knifeObj.transform.position = attackPos.position;
    }
}
