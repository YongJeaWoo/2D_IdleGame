using SingletonBase.DontDestroySingleton;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonBase<UIManager>
{
    [Header("라운드 표시 텍스트")]
    [SerializeField] private TextMeshProUGUI roundText;

    [Header("Hp바")]
    [SerializeField] private Image payerHpBar;
    [SerializeField] private TextMeshProUGUI payerHpBarText;
    [SerializeField] private Image enemyHpBar;
    [SerializeField] private TextMeshProUGUI enemyHpBarText;

    [Header("UI 캔버스")]
    [SerializeField] private GameObject UICanvas;

    private float lerpSpeed = 10f;

    private void Start()
    {
        payerHpBar.fillAmount = 1;
        payerHpBarText.text = "";
        enemyHpBar.fillAmount = 1;
        enemyHpBarText.text = "";
    }

    private void RefreshHpBar(Image hpBar, TextMeshProUGUI hpBarText, BigInteger currentHp, BigInteger maxHp)
    {
        float targetFillAmount = maxHp == 0 ? 0 : (float)(double)currentHp / (float)(double)maxHp;
        hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, targetFillAmount, Time.deltaTime * lerpSpeed);
        hpBarText.text = currentHp.ToString();
    }

    public void RefreshEnemyHp(BigInteger currentHp, BigInteger maxHp)
    {
        RefreshHpBar(enemyHpBar, enemyHpBarText, currentHp, maxHp);
    }

    public void RefreshPlayerHp(BigInteger currentHp, BigInteger maxHp)
    {
        RefreshHpBar(payerHpBar, payerHpBarText, currentHp, maxHp);
    }

    public TextMeshProUGUI GetRoundText() => roundText;
    public GameObject GetCanvas() => UICanvas;
}
