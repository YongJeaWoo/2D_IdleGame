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

    public BigInteger GetHasGold() => hasGold;
    public BigInteger SetHasGold(BigInteger value) => hasGold = value;
    public BigInteger GetHasOre() => hasOre;
    public BigInteger SetHasOre(BigInteger value) => hasOre = value;
}
