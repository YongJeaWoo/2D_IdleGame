using System.Collections;
using System.Numerics;
using TMPro;
using UnityEngine;

public class DamageText : NumberFormatterText
{
    [Header("�ؽ�Ʈ ��� �ӵ�")]
    [SerializeField] private float upSpeed;
    [Header("ǥ�� ���� �ð�")]
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
