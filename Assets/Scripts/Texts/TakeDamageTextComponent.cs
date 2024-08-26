using System.Numerics;
using UnityEngine;

public class TakeDamageTextComponent : MonoBehaviour
{
    [Header("대미지 텍스트 프리팹")]
    [SerializeField] private GameObject damagedTextPrefab;
    [Header("Y값 포지션 잡기")]
    [SerializeField] private float plusYValue;

    private void Start()
    {
        ObjectPoolManager.Instance.InitObjectPool(damagedTextPrefab);
    }

    public void ShowDamagedText(BigInteger damage)
    {
        var uiCanvas = UIManager.Instance.uiCanvas;

        UnityEngine.Vector3 yValue = new(transform.position.x, transform.position.y + plusYValue, transform.position.z);

        GameObject damageText = ObjectPoolManager.Instance.GetToPool(damagedTextPrefab, uiCanvas);
        damageText.transform.SetParent(uiCanvas);

        UnityEngine.Vector2 screenPos = Camera.main.WorldToScreenPoint(yValue);
        damageText.transform.position = screenPos;

        var text = damageText.GetComponent<DamageText>();
        text.ShowTakeDamage(damage);
    }
}
