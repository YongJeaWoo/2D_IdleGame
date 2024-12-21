using SingletonBase.DontDestroySingleton;
using System;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonBase<UIManager>
{
    [Header("UI용 캔버스")]
    [SerializeField] private GameObject uiCanvas;

    [SerializeField] private BottomCollector bottmCollection;

    [Header("라운드 표시 텍스트")]
    [SerializeField] private TextMeshProUGUI roundText;
    [Header("던전 표시 텍스트")]
    [SerializeField] private TextMeshProUGUI dungeonText;

    [SerializeField] public GameObject dungeonEndPanel;

    [Header("UI 정보들")]
    [SerializeField] private Image[] hpBars;                                    // 플레이어 0, 적 1
    [SerializeField] private TextMeshProUGUI[] hpTexts;             // 플레이어 0, 적 1
    [SerializeField] private TextMeshProUGUI[] nameTexts;         // 플레이어 0, 적 1 

    private float lerpSpeed = 10f;

    [Header("이팩트용 UI 캔버스")]
    public Transform effectUICanvas;

    [Header("재화 관련 정보")]
    [SerializeField] private TextMeshProUGUI[] possessText;

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

    public void UpdatePossessText(TextMeshProUGUI showText, BigInteger amount)
    {
        string formatText = FormatterText(amount);
        showText.text = formatText;
    }

    public string FormatterText(BigInteger value)
    {
        if (value == 0)
        {
            return "0";
        }

        string[] units = { "", "만", "억", "조", "경", "해", "자", "양", "구", "간", "정" };
        List<string> parts = new List<string>();

        int totalDigits = value.ToString().Length;

        // 표시할 최대 단위 인덱스 계산 (최대 2단위)
        int maxUnitsToShow = 2;
        int unitIndex = Math.Min((totalDigits - 1) / 4, units.Length - 1);

        // 표시할 단위 인덱스 결정
        int startUnitIndex = Math.Max(unitIndex - maxUnitsToShow + 1, 0);

        // 현재 단위와 상위 단위까지 처리
        while (unitIndex >= startUnitIndex && value > 0)
        {
            // 현재 단위에 대한 값 추출
            BigInteger divisor = BigInteger.Pow(10, unitIndex * 4);
            BigInteger currentValue = value / divisor;
            value %= divisor;

            // 값이 0이 아니면 추가
            if (currentValue > 0)
            {
                string formattedValue = currentValue.ToString();
                if (formattedValue != "0")
                {
                    parts.Add($"{formattedValue}{units[unitIndex]}");
                }
            }

            unitIndex--;
        }

        string result = string.Join(" ", parts).Trim();
        return result;
    }

    public void ToggleUICanvas(bool isOn)
    {
        uiCanvas.SetActive(isOn);
    }

    public Image[] GetHpBars() => hpBars;
    public TextMeshProUGUI[] GetHpTexts() => hpTexts;
    public TextMeshProUGUI GetDungeonText() => dungeonText;
    public TextMeshProUGUI GetRoundText() => roundText;
    public TextMeshProUGUI[] GetPossessText() => possessText;
    public TextMeshProUGUI[] GetNameText() => nameTexts;
    public BottomCollector GetBottomCollection() => bottmCollection;
}
