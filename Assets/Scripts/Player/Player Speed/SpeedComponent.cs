using UnityEngine;

public class SpeedComponent : MonoBehaviour
{
    [Header("�����̴� �ӵ�")]
    [SerializeField] private float moveSpeed;

    public float GetMoveSpeed() => moveSpeed;
}
