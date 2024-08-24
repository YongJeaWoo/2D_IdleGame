using SingletonBase.DontDestroySingleton;
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

    private void Start()
    {
        payerHpBar.fillAmount = 1;
        payerHpBarText.text = "";
        enemyHpBar.fillAmount = 1;
        enemyHpBarText.text = "";
    }

    public void RefreshEnemyHp(int currentHp, int maxHp)
    {
        enemyHpBar.fillAmount = (float)currentHp / maxHp;

        enemyHpBarText.text = currentHp.ToString();
    }

    public void RefreshPlayerHp(int currentHp, int maxHp)
    {
        payerHpBar.fillAmount = (float)currentHp / maxHp;

        payerHpBarText.text = currentHp.ToString();
    }

    public TextMeshProUGUI GetRoundText() => roundText;
}
