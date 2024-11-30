using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoTextButton : MonoBehaviour
{
    protected Button button;
    protected TextMeshProUGUI toggleText;
    protected bool isAutoPlay;

    protected virtual void Start()
    {
        InitButton();
    }

    protected virtual void InitButton()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ToggleActiveButton);

        var obj = UIManager.Instance.gameObject;
        obj.GetComponentInChildren<FunctionBarComponent>();

        toggleText = GetComponentInChildren<TextMeshProUGUI>();
        toggleText.text = $"OFF";
    }

    public virtual void ToggleActiveButton()
    {
        isAutoPlay = !isAutoPlay;
        toggleText.text = isAutoPlay ? $"ON" : $"OFF";
    }
}
