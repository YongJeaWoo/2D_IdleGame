using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoTextButton : MonoBehaviour
{
    protected Button button;
    protected TextMeshProUGUI toggleText;
    protected bool isAutoPlay;

    protected virtual void Awake()
    {
        InitButton();
    }

    protected virtual void InitButton()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ToggleActiveButton);

        var obj = UIManager.Instance.gameObject;
        var division = obj.GetComponentInChildren<BottomDivisionComponent>();
        var function = division.GetFunctionBar();

        toggleText = GetComponentInChildren<TextMeshProUGUI>();
        toggleText.text = $"OFF";
    }

    public virtual void ToggleActiveButton()
    {
        isAutoPlay = !isAutoPlay;
        toggleText.text = isAutoPlay ? $"ON" : $"OFF";
    }
}
