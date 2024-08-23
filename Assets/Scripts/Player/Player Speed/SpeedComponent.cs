using System;
using UnityEngine;

public class SpeedComponent : MonoBehaviour
{
    [Header("플레이어 관련 속도")]
    [Range(0.8f, 2f)]
    [SerializeField] private float speed;

    public float GetSpeed() => speed;
}
