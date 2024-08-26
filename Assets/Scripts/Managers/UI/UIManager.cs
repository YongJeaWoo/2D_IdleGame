using SingletonBase.DontDestroySingleton;
using System.Collections;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonBase<UIManager>
{
    [Header("라운드 표시 텍스트")]
    [SerializeField] private TextMeshProUGUI roundText;

    [Header("체력 관련 정보")]
    [SerializeField] private Image[] hpBars;                                // 플레이어 0, 적 1
    [SerializeField] private TextMeshProUGUI[] hpTexts;          // 플레이어 0, 적 1

    private float lerpSpeed = 10f;

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

    public Image[] GetHpBars() => hpBars;
    public TextMeshProUGUI[] GetHpTexts() => hpTexts;
    public TextMeshProUGUI GetRoundText() => roundText;
}
