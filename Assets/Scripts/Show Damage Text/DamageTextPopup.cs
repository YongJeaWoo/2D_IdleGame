using System.Numerics;
using UnityEngine;

public class DamageTextPopup : MonoBehaviour
{
    [Header("������ �ؽ�Ʈ ������")]
    [SerializeField] private GameObject damageTextPrefab;

    [Header("������ �˾��� ǥ���� Canvas")]
    [SerializeField] private Transform canvasTransform;

    [Header("���� ��ġ ���� ��ġ")]
    [SerializeField] private float plusValue;

    public void ShowDamageText(BigInteger damage, UnityEngine.Vector3 position)
    {
        position.y += plusValue;

        GameObject damageText = Instantiate(damageTextPrefab, canvasTransform);

        UnityEngine.Vector2 screenPosition = Camera.main.WorldToScreenPoint(position);
        damageText.transform.position = screenPosition;

        var text = damageText.GetComponent<DamageText>();
        text.ShowText(damage);
    }
}
