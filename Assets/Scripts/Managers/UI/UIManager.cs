using SingletonBase.DontDestroySingleton;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonBase<UIManager>
{
    [Header("���� ǥ�� �ؽ�Ʈ")]
    [SerializeField] private TextMeshProUGUI roundText;

    [Header("ü�� ���� ����")]
    [SerializeField] private Image[] hpBars;                                // �÷��̾� 0, �� 1
    [SerializeField] private TextMeshProUGUI[] hpTexts;          // �÷��̾� 0, �� 1

    private float lerpSpeed = 10f;

    [Header("����Ʈ�� UI ĵ����")]
    public Transform uiCanvas;

    [Header("��ȭ ���� ����")]
    [SerializeField] private TextMeshProUGUI[] possessText;
    // gold, ore, other ������ ����
    private BigInteger[] possess = new BigInteger[3];

    public void InitHpImage()
    {
        for (int i = 0; i < hpBars.Length; i++)
        {
            hpBars[i].fillAmount = 1;
            hpTexts[i].text = string.Empty;
        }
    }

    public void RefreshHpBar(BaseHealth targetHealth, BigInteger currentHp, BigInteger maxHp)
    {
        if (targetHealth == null) return;

        var healthImage = targetHealth.GetHealthImage();
        var healthText = targetHealth.GetHealthText();
        float targetFillAmount = maxHp == 0 ? 0 : (float)(double)currentHp / (float)(double)maxHp;

        healthImage.fillAmount = Mathf.Lerp(healthImage.fillAmount, targetFillAmount, Time.deltaTime * lerpSpeed);
        healthText.text = currentHp.ToString();
    }

    // ��ȭ �ʱ�ȭ
    public void InitializePossess()
    {
        for (int i = 0; i < possess.Length; i++)
        {
            possess[i] = BigInteger.Zero;
            possessText[i].text = "0";
        }
    }

    public void UpdatePossessText(TextMeshProUGUI showText, BigInteger amount)
    {
        string formatText = FormatterText(amount);
        showText.text = formatText;
    }

    public string FormatterText(BigInteger value)
    {
        string[] units = { "", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��" };
        List<string> parts = new List<string>();

        int totalDigits = value.ToString().Length;

        if (totalDigits > 8)
        {
            value = value / BigInteger.Pow(10, totalDigits - 8);
        }

        int startingUnitIndex = (totalDigits - 1) / 4;

        int unitIndex = 0;
        while (value > 0 && startingUnitIndex - unitIndex >= 0)
        {
            BigInteger currentValue = value % 10000;

            if (currentValue > 0 || parts.Count > 0)
            {
                string formattedValue = currentValue.ToString();
                if (formattedValue != "0000")
                {
                    string displayValue = formattedValue.TrimStart('0');
                    parts.Insert(0, $"{displayValue}{units[startingUnitIndex - unitIndex]}");
                }
            }

            value /= 10000;
            unitIndex++;
        }

        string result = string.Join(" ", parts).Trim();
        return result;
    }

    public Image[] GetHpBars() => hpBars;
    public TextMeshProUGUI[] GetHpTexts() => hpTexts;
    public TextMeshProUGUI GetRoundText() => roundText;
    public TextMeshProUGUI[] GetPossessText() => possessText;
}
