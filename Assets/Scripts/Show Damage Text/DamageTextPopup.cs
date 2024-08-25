using System.Numerics;
using UnityEngine;

public class DamageTextPopup : MonoBehaviour
{
    [Header("������ �ؽ�Ʈ ������")]
    [SerializeField] private GameObject damageTextPrefab;

    [Header("���� ��ġ ���� ��ġ")]
    [SerializeField] private float plusValue;

    [Header("UI�� �ø� ĵ����")]
    [SerializeField] private Transform canvasTransform;

    private void Start()
    {
        ObjectPoolManager.Instance.InitObjectPool(damageTextPrefab);
    }

    public void ShowDamageText(BigInteger damage, UnityEngine.Vector3 position)
    {
        position.y += plusValue;

        GameObject damageText = ObjectPoolManager.Instance.GetToPool(damageTextPrefab, canvasTransform);
        damageText.transform.SetParent(canvasTransform);

        UnityEngine.Vector2 screenPosition = Camera.main.WorldToScreenPoint(position);
        damageText.transform.position = screenPosition;

        var text = damageText.GetComponent<DamageText>();
        text.ShowText(damage);
    }
}
