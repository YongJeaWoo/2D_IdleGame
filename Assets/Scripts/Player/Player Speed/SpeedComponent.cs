using UnityEngine;

public class SpeedComponent : MonoBehaviour
{
    [Header("움직이는 속도")]
    [SerializeField] private float moveSpeed;

    public float GetMoveSpeed() => moveSpeed;
}
