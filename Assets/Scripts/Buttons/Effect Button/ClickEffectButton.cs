using UnityEngine;
using UnityEngine.UI;

public class ClickEffectButton : MonoBehaviour
{
    [Header("선택된 색상")]
    [SerializeField] private Color selectColor;

    [Header("선택되지 않은 색상")]
    [SerializeField] private Color deSelectColor;

    public void SelectButton(Button button)
    {
        ColorBlock buttonColor = button.colors;
        buttonColor.selectedColor = selectColor;
        button.colors = buttonColor;
    }

    public void DeSelectButton(Button button)
    {
        ColorBlock buttonColor = button.colors;
        buttonColor.selectedColor = deSelectColor;
        button.colors = buttonColor;
    }
}
