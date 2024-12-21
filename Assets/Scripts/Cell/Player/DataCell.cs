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
    protected PlayerPossessionsController possessionController;

    [SerializeField] protected string possessName;

    public static event Action ClickButton;

    private readonly string NoMoney = $"Warning Panel";
    private readonly string NoMoneyExplainText = $"현재 자원이 부족합니다.";

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
        possessionController = PlayerManager.Instance.GetPossessionsController();
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
        if (!possessionController.SpendPossess(possessName, upgradeCost))
        {
            var panel = PopupManager.Instance.InstantPopup(NoMoney);
            var warningPanel = panel.GetComponent<WarningPanel>();
            warningPanel.SetAlramPanelText(NoMoneyExplainText);
            return;
        }

        ExecuteClick();
        UpgradeCost();
        UpdateDisplay();
        ClickButton?.Invoke();
    }

    public abstract void InitInfo();
    public abstract void UpdateDisplay();
    public abstract void ExecuteClick();
}
