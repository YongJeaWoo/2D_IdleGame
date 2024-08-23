using UnityEngine;

public class PlayerAttack : BaseAttack
{
    [SerializeField] private Transform attackPos;

    [SerializeField] private GameObject weapon;
    
    private void Start()
    {
        ObjectPoolManager.Instance.InitObjectPool(weapon);
    }

    public override void AttackAnimation()
    {
        ObjectPoolManager.Instance.GetToPool(weapon, attackPos);
    }
}
