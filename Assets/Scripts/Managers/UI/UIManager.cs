using SingletonBase.DontDestroySingleton;
using System.Collections;
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

    private void Start()
    {
        InitPossess();
    }

    private void InitPossess()
    {
        // �ʱ� ��ȭ �� ����
        possess[0] = BigInteger.Zero; // Gold �ʱ�ȭ
        possess[1] = BigInteger.Zero; // Ore �ʱ�ȭ
        possess[2] = BigInteger.Zero; // Other �ʱ�ȭ

        // UI �ʱ�ȭ
        UpdatePossessText();
    }

    public void UpdatePossessText()
    {
        for (int i = 0; i < possessText.Length && i < possess.Length; i++)
        {
            possessText[i].text = $"{possess[i]}";
        }
    }

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
    public BigInteger[] GetPossess() => possess;
}
