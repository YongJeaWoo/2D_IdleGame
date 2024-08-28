using System;
using System.Numerics;
using UnityEngine;

public class SpeedComponent : MonoBehaviour
{
    [Header("플레이어 관련 속도")]
    [Range(0.8f, 2f)]
    [SerializeField] private float speed;

    [Header("피버타임 스피드 증가 배수")]
    [SerializeField] private float multipleSpeed;

    [Header("피버타임 시간")]
    [SerializeField] private float feverTime;

    private BigInteger feverMulIndex = 2;

    private BigInteger index;
    private BigInteger lastFeverIndex = -1;
    private BackgroundController bgController;
    private Animator anim;

    private float originalSpeed;
    private float originAnimSpeed;
    private bool feverActive = false;

    private void Start()
    {
        bgController = FindAnyObjectByType<BackgroundController>();
        anim = GetComponent<Animator>();

        originalSpeed = speed;
        originAnimSpeed = anim.speed;
    }

    private void Update()
    {
        index = LevelManager.Instance.GetCurrentRound();

        if (!feverActive && index % feverMulIndex == 0 && index != lastFeverIndex)
        {
            SpeedUp();
        }
    }

    private void SpeedUp()
    {
        feverActive = true;
        lastFeverIndex = index;
        speed *= multipleSpeed;

        if (anim != null)
        {
            anim.speed *= multipleSpeed;
        }

        bgController.BG_SpeedControll(this);
        Invoke("SpeedDown", feverTime);
    }

    private void SpeedDown()
    {
        speed = originalSpeed;

        if (anim != null)
        {
            anim.speed = originAnimSpeed;
        }

        bgController.BG_SpeedControll(this);
        feverActive = false;
    }

    public float GetSpeed() => speed;
}
