using System.Numerics;
using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerPossessionsController possessionsController;

    public PlayerPossessionsController GetPossessionsController()
    {
        return player.GetComponent<PlayerPossessionsController>();
    }

    public BigInteger GetAttack()
    {
        return player.GetComponent<PlayerAttack>().GetAtk();
    }

    public void SetAttack(BigInteger newAtk)
    {
        player.GetComponent<PlayerAttack>().SetAtk(newAtk);
    }

    public BigInteger GetMaxHp()
    {
        return player.GetComponent<PlayerHealth>().GetMaxHp();
    }

    public void SetMaxHp(BigInteger newMaxHp)
    {
        player.GetComponent<PlayerHealth>().SetMaxHp(newMaxHp);
    }

    public float GetSpeed()
    {
        return player.GetComponent<SpeedComponent>().GetSpeed();
    }

    public void SetSpeed(float newSpeed)
    {
        player.GetComponent<SpeedComponent>().UpValue(newSpeed);
    }

    public GameObject GetPlayer() => player;

}
