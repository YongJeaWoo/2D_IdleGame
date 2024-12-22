using System.Numerics;

public class DungeonHealth : BaseHealth
{
    protected override void Start()
    {
        base.Start();
        maxHp = BigInteger.Parse(maxHpString);
        SetValues();
        SetCurrentHpToMaxHp();
    }

    public override void Hit(BigInteger attackPoint)
    {
        AudioManager.Instance.PlaySFX(hitSound);
        currentHp -= attackPoint;
        takeDamage.ShowDamagedText(attackPoint);
        var hitEffect = ObjectPoolManager.Instance.GetToPool(hitEffectPrefab);
        UnityEngine.Vector3 effectPosition = transform.position;
        effectPosition.y += 0.5f;
        hitEffect.transform.position = effectPosition;

        if (currentHp % 10 == 0)
        {
            var drop = GetComponent<DropPossessItem>();
            drop.DropItem();
        }
    }
}
