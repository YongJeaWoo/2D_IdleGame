using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using TMPro;

public abstract class DropPossessItem : MonoBehaviour, IItemDropper
{
    // ��ȭ ȹ�淮
    protected TextMeshProUGUI PossessText;
    protected PlayerSystem playerSystem;
    protected PlayerPossessionsController possessionsController;

    protected virtual void Start()
    {
        InitPossess();
        playerSystem = FindObjectOfType<PlayerSystem>();
        var player = playerSystem.GetPlayer();
        possessionsController = player.GetComponent<PlayerPossessionsController>();
    }

    // ������ ���
    public abstract void DropItem();

    protected abstract void InitPossess();
}
