using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using TMPro;

public abstract class DropPossessItem : MonoBehaviour, IItemDropper
{
    // ¿Á»≠ »πµÊ∑Æ
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

    // æ∆¿Ã≈€ µÂ∂¯
    public abstract void DropItem();

    protected abstract void InitPossess();
}
