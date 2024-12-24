using System.Numerics;
using UnityEngine;

public class PlayerPossessionsController : MonoBehaviour
{
    private BigInteger hasGold;
    private BigInteger hasOre;

    private PlayerManager playerManager;

    void Start()
    {
        InitPossessItems();
    }

    private void InitPossessItems()
    {
        playerManager = PlayerManager.Instance;

        BigInteger initialGold = playerManager.GetPlayerPossessData().LoadPossess("gold");
        BigInteger initiialOre = playerManager.GetPlayerPossessData().LoadPossess("ore");
        BigInteger initialCapsule = playerManager.GetPlayerPossessData().LoadPossess("capsule");

        UIManager.Instance.UpdatePossessText(UIManager.Instance.GetPossessText()[0], initialGold);
        UIManager.Instance.UpdatePossessText(UIManager.Instance.GetPossessText()[1], initiialOre);
        UIManager.Instance.UpdatePossessText(UIManager.Instance.GetPossessText()[2], initialCapsule);
    }

    public bool SpendPossess(string possess, BigInteger amount)
    {
        if (playerManager.GetPlayerPossessData().TrySpendPossess(possess, amount))
        {
            BigInteger updatedValue = playerManager.GetPlayerPossessData().LoadPossess(possess);
            int uiIndex = GetResourceUIIndex(possess);
            if (uiIndex >= 0)
            {
                UIManager.Instance.UpdatePossessText(UIManager.Instance.GetPossessText()[uiIndex], updatedValue);
            }
            return true;
        }

        return false;
    }

    public void SavePossess(string possess, BigInteger amount)
    {
        playerManager.GetPlayerPossessData().SavePossess(possess, amount);
        int uiIndex = GetResourceUIIndex(possess);
        if (uiIndex >= 0)
        {
            UIManager.Instance.UpdatePossessText(UIManager.Instance.GetPossessText()[uiIndex], amount);
        }
    }

    public BigInteger LoadPossess(string possess) => playerManager.GetPlayerPossessData().LoadPossess(possess);

    private int GetResourceUIIndex(string resourceName)
    {
        switch (resourceName)
        {
            case "gold": return 0;
            case "ore": return 1;
            case "capsule": return 2;
            default: return -1;
        }
    }

    public BigInteger GetHasGold() => hasGold;
    public BigInteger SetHasGold(BigInteger value) => hasGold = value;
    public BigInteger GetHasOre() => hasOre;
    public BigInteger SetHasOre(BigInteger value) => hasOre = value;
}
