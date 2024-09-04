using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoProduceButton : MonoBehaviour
{
    private Button myButton;
    private KnifeCollectionBar knifeBar;
    private TextMeshProUGUI toggleText;

    private void Awake()
    {
        InitButton();
    }

    private void InitButton()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(ToggleActiveButton);

        var obj = UIManager.Instance.gameObject;
        var division = obj.GetComponentInChildren<BottomDivisionComponent>();
        var function = division.GetFunctionBar();
        knifeBar = function.GetKnifeCollectBar();

        toggleText = GetComponentInChildren<TextMeshProUGUI>();
        toggleText.text = $"OFF";
    }

    public void ToggleActiveButton()
    {
        knifeBar.IsAutoPlay = !knifeBar.IsAutoPlay;
        toggleText.text = knifeBar.IsAutoPlay ? $"ON" : $"OFF";
    }
}
