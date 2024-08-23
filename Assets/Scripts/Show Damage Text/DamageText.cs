using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [Header("TextMeshPro ������Ʈ ����")]
    [SerializeField] private TextMeshProUGUI dmgText;

    [Header("�ؽ�Ʈ ��� �ӵ�")]
    [SerializeField] private float upSpeed;

    [Header("�ؽ�Ʈ ���� �ð�")]
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
        string[] units = { "", "��", "��", "��", "��" };
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
