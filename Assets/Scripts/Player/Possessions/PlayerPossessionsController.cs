using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerPossessionsController : MonoBehaviour
{
    private BigInteger hasGold;
    private BigInteger hasOre;

    void Start()
    {
        UIManager.Instance.InitializePossess();
    }

    public bool SpendGold(BigInteger amount)
    {
        if (hasGold >= amount)
        {
            hasGold -= amount;
            UIManager.Instance.UpdatePossessText(UIManager.Instance.GetPossessText()[0], hasGold);
            return true;
        }
        return false;
    }

    public bool SpendOre(BigInteger amount)
    {
        if (hasOre >= amount)
        {
            hasOre -= amount;
            UIManager.Instance.UpdatePossessText(UIManager.Instance.GetPossessText()[1], hasOre);
            return true;
        }
        return false;
    }

    public BigInteger GetHasGold() => hasGold;
    public BigInteger SetHasGold(BigInteger value) => hasGold = value;
    public BigInteger GetHasOre() => hasOre;
    public BigInteger SetHasOre(BigInteger value) => hasOre = value;
}
