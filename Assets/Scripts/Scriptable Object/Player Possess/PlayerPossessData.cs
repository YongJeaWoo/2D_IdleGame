using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[CreateAssetMenu(fileName ="PossessData", menuName = "Game/PosessData")]
public class PlayerPossessData : ScriptableObject
{
    private Dictionary<string, BigInteger> possessItems = new Dictionary<string, BigInteger>();

    public void SavePossess(string possess, BigInteger getValue)
    {
        if (possessItems.ContainsKey(possess))
        {
            possessItems[possess] = getValue;
        }
        else
        {
            possessItems.Add(possess, getValue);
        }
    }

    public BigInteger LoadPossess(string possess) => possessItems.TryGetValue(possess, out BigInteger value) ? value : BigInteger.Zero;

    public bool TrySpendPossess(string possess, BigInteger amount)
    {
        if (possessItems.TryGetValue(possess, out BigInteger currentValue) && currentValue >= amount)
        {
            possessItems[possess] = currentValue - amount;
            return true;
        }

        return false;
    }
}
