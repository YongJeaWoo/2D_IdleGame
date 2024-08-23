using SingletonBase.DontDestroySingleton;
using TMPro;
using UnityEngine;

public class UIManager : SingletonBase<UIManager>
{
    [Header("���� ǥ�� �ؽ�Ʈ")]
    [SerializeField] private TextMeshProUGUI roundText;

    public TextMeshProUGUI GetRoundText() => roundText;
}
