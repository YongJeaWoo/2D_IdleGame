using System;
using System.Numerics;
using UnityEngine;

public class SpeedComponent : MonoBehaviour
{
    [Header("플레이어 관련 속도")]
    [Range(0.8f, 2f)]
    [SerializeField] private float speed;

    private BackgroundController bgController;
    private Animator anim;

    private float saveSpeed, saveAnimSpeed;

    private void Start()
    {
        bgController = FindAnyObjectByType<BackgroundController>();
        anim = GetComponent<Animator>();

        saveSpeed = speed;
        saveAnimSpeed = anim.speed;
    }

    public void SpeedUp(float multipleSpeed, float time)
    {
        UpValue(multipleSpeed);

        bgController.BG_SpeedControll(this);
        Invoke("SpeedDown", time);
    }

    private void SpeedDown()
    {
        RestoreValue();

        bgController.BG_SpeedControll(this);
    }

    private void UpValue(float upValue)
    {
        speed *= upValue;
        anim.speed *= upValue;
    }

    private void RestoreValue()
    {
        speed = saveSpeed;
        anim.speed = saveAnimSpeed;
    }

    public float GetSpeed() => speed;
}
