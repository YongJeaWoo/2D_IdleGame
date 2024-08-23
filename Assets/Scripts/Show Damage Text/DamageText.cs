using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [Header("TextMeshPro 컴포넌트 참조")]
    [SerializeField] private TextMeshProUGUI dmgText;

    [Header("텍스트 상승 속도")]
    [SerializeField] private float upSpeed;

    [Header("텍스트 지속 시간")]
    [SerializeField] private float duration;

    public void ShowText(BigInteger damage)
    {
        dmgText.text = SetText(damage);
        StartCoroutine(TextCoroutine());
    }

    private IEnumerator TextCoroutine()
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            transform.Translate(UnityEngine.Vector3.up * upSpeed * Time.deltaTime);
            
            yield return null;
        }
        
        Destroy(gameObject);
    }

    private string SetText(BigInteger value)
    {
        string[] units = { "", "만", "억", "조", "경" };
        List<string> parts = new List<string>();
        int unitIndex = 0;

        while (value > 0 && unitIndex < units.Length)
        {
            BigInteger currentValue = value % 10000;

            if (currentValue > 0)
            {
                parts.Insert(0, $"{currentValue}{units[unitIndex]}");
            }
            value /= 10000;
            unitIndex++;
        }

        return string.Join("", parts);
    }
}
