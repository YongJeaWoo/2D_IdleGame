using System;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class DataCell : MonoBehaviour
{
    [SerializeField] protected Image iconImage;
    [SerializeField] protected TextMeshProUGUI infoNameText;
    [SerializeField] protected TextMeshProUGUI upgradeCostText;
    [SerializeField] protected TextMeshProUGUI numericalText;

    protected float upgradeMultiple;
    protected BigInteger upgradeCost;
    protected BigInteger displayValue;
    protected PlayerSystem playerSystem;
    protected PlayerPossessionsController possessionController;

    public static event Action ClickButton;

    protected virtual void Awake()
    {
        InitInfo();
    }

    protected virtual void OnEnable()
    {
        FindSystem();
    }

    protected virtual void Start()
    {
        upgradeCost = 100;
        UpdateUpgradeCostText();
    }

    private void FindSystem()
    {
        playerSystem = FindObjectOfType<PlayerSystem>();
        possessionController = playerSystem.GetPossessionsController();
    }

    public void UpgradeCost()
    {
        upgradeCost *= 2;
        UpdateUpgradeCostText();
    }

    private void UpdateUpgradeCostText()
    {
        upgradeCostText.text = upgradeCost.ToString();
    }

    public virtual void OnButtonClick()
    {
        if (!possessionController.SpendGold(upgradeCost)) return;

        ExecuteClick();
        UpgradeCost();
        UpdateDisplay();
        ClickButton?.Invoke();
    }

    public abstract void InitInfo();
    public abstract void UpdateDisplay();
    public abstract void ExecuteClick();
}
