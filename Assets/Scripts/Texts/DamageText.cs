using System.Collections;
using System.Numerics;
using TMPro;
using UnityEngine;

public class DamageText : NumberFormatterText
{
    [Header("텍스트 상승 속도")]
    [SerializeField] private float upSpeed;
    [Header("표시 지속 시간")]
    [SerializeField] private float duration;

    private TextMeshProUGUI dmgText;

    private void Awake()
    {
        dmgText = GetComponent<TextMeshProUGUI>();
    }

    public void ShowTakeDamage(BigInteger damage)
    {
        dmgText.text = UIManager.Instance.FormatterText(damage);
        StartCoroutine(TextCoroutine());
    }

    private IEnumerator TextCoroutine()
    {
        float timer = 0;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            transform.Translate(upSpeed * Time.deltaTime * UnityEngine.Vector3.up);

            yield return null;
        }

        ObjectPoolManager.Instance.ReleaseToPool(gameObject);
    }
}
