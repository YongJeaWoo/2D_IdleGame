using UnityEngine;

public class EnemyHealth : BaseHealth
{
    [SerializeField] private GameObject deathParticle;

    protected override void Death()
    {
        //Instantiate(deathParticle, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
