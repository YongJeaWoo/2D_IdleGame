using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class NumberFormatterText : MonoBehaviour
{
    public string FormatterText(BigInteger value)
    {
        string[] units = { "", "만", "억", "조", "경" };
        List<string> parts = new List<string>();
        int unitIndex = 0;

        while (value > 0 && unitIndex < units.Length)
        {
            BigInteger currentValue = value % 10000;

            if (currentValue > 0)
            {
                parts.Insert(0, $"{currentValue:000}{units[unitIndex]}");
            }
            else
            {
                parts.Insert(0, $"{currentValue}{units[unitIndex]}");
            }

            value /= 10000;
            unitIndex++;
        }

        string result = string.Join(" ", parts);
        result = result.TrimStart('0');

        return result;
    }
}
