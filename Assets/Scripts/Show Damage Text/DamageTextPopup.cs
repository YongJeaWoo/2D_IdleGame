using System.Numerics;
using UnityEngine;

public class DamageTextPopup : MonoBehaviour
{
    [Header("데미지 텍스트 프리팹")]
    [SerializeField] private GameObject damageTextPrefab;

    [Header("생성 위치 보간 수치")]
    [SerializeField] private float plusValue;

    [Header("UI를 올릴 캔버스")]
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
