using SingletonBase.DontDestroySingleton;
using System;
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

    public event Action<IItemDropper> OnDrop;

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
            UpdatePossessText(i, "0");
        }
    }

    public void DropPossessEvent(IItemDropper item)
    {
        OnDrop.Invoke(item);
    }

    public void UpdatePossessText(int index, string formattedText)
    {
        if (index >= 0 && index < possessText.Length)
        {
            possessText[index].text = formattedText;
        }
    }

    public Image[] GetHpBars() => hpBars;
    public TextMeshProUGUI[] GetHpTexts() => hpTexts;
    public TextMeshProUGUI GetRoundText() => roundText;
    public BigInteger[] GetPossess() => possess;
}
