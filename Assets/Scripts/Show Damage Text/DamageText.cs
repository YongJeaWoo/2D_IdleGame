using System.Collections;
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
        dmgText.text = damage.ToString();
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
}
