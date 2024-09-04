using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI statNameText;
    [SerializeField] protected TextMeshProUGUI costText;
    [SerializeField] protected TextMeshProUGUI statIncreaseAmountText;
    protected BigInteger cost;
    protected BigInteger statIncreaseAmount;
    public static event Action OnEnhance;
    protected PlayerSystem playerSystem;

    protected virtual void Start()
    {
        playerSystem = FindObjectOfType<PlayerSystem>();
    }

    public void EnhanceStat()
    {
        cost += 100;
        UpdateInfo();
    }

    public void InitSall(SallSOJ sallData)
    {
        iconImage.sprite = sallData.Icon;
        statNameText.text = sallData.StatName;
        cost = 100;
        costText.text = cost.ToString();
        statIncreaseAmountText.text = statIncreaseAmount.ToString();
    }

    public void UpdateInfo()
    {
        costText.text = cost.ToString();
        statIncreaseAmountText.text = statIncreaseAmount.ToString();
    }

    public virtual void OnButtonClick()
    {
        EnhanceStat();
        OnEnhance?.Invoke();
    }
}
