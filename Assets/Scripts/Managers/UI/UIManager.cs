using SingletonBase.DontDestroySingleton;
using TMPro;
using UnityEngine;

public class UIManager : SingletonBase<UIManager>
{
    [Header("라운드 표시 텍스트")]
    [SerializeField] private TextMeshProUGUI roundText;

    public TextMeshProUGUI GetRoundText() => roundText;
}
