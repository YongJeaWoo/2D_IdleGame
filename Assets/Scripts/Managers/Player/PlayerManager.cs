using SingletonBase.DontDestroySingleton;
using System;
using System.Collections;
using System.Numerics;
using UnityEngine;

public class PlayerManager : SingletonBase<PlayerManager>
{
    [SerializeField] private PlayerPossessData playerPossessData;

    private GameObject player;
    public event Action OnPlayerReady;

    public void FindPlayer()
    {
        StartCoroutine(FindPlayerSceneLoad());
    }

    private IEnumerator FindPlayerSceneLoad()
    {
        yield return new WaitForEndOfFrame();

        while (player == null)
        {
            player = GameObject.FindWithTag("Player");

            ParallaxBackground[] parallaxsBG = FindObjectsOfType<ParallaxBackground>();
            foreach (var parallaxBG in parallaxsBG)
            {
                parallaxBG.InitValue(this);
            }

            yield return new WaitForEndOfFrame();

            var collector = UIManager.Instance.GetBottomCollection();
            collector.Initialize(this);

            yield return new WaitForEndOfFrame();

        }

        OnPlayerReady?.Invoke();

        var possessController = GetPossessionsController();
        GetPossessionsLoad(possessController);
    }

    public void GetPossessionsLoad(PlayerPossessionsController possessController)
    {
        possessController.LoadPossess("gold");
        possessController.LoadPossess("ore");
        possessController.LoadPossess("capsule");
    }

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

    public BigInteger SetCurrentHp(BigInteger newHp)
    {
        var pHealth = player.GetComponent<PlayerHealth>();
        var currentHp = pHealth.GetCurrentHp();
        var finalHealth = currentHp + newHp;
        var currentHpChanging = pHealth.SetCurrentHp(finalHealth);
        return currentHpChanging;
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
        player.GetComponent<SpeedComponent>().SetSpeed(newSpeed);
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public PlayerPossessData GetPlayerPossessData() => playerPossessData;

}
