using System.Numerics;
using UnityEngine;

public class DamageTextPopup : MonoBehaviour
{
    [Header("데미지 텍스트 프리팹")]
    [SerializeField] private GameObject damageTextPrefab;

    [Header("데미지 팝업을 표시할 Canvas")]
    [SerializeField] private Transform canvasTransform;

    [Header("생성 위치 보간 수치")]
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
