using UnityEngine;
using UnityEngine.UI;

public class ClickEffectButton : MonoBehaviour
{
    [Header("���õ� ����")]
    [SerializeField] private Color selectColor;

    [Header("���õ��� ���� ����")]
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
