using System;
using UnityEngine;

public class SpeedComponent : MonoBehaviour
{
    [Header("�÷��̾� ���� �ӵ�")]
    [Range(0.8f, 2f)]
    [SerializeField] private float speed;

    public float GetSpeed() => speed;
}
