using UnityEngine;
using UnityEngine.UI;

public class ClickEffectButton : MonoBehaviour
{
    [Header("선택 되었을 때 해제 표시할 이팩트")]
    [SerializeField] private Sprite changeImage;
    [Header("선택 되었을 때 표시할 오브젝트")]
    [SerializeField] private GameObject changeObject;
    [Header("해제 되었을 때 표시할 오브젝트")]
    [SerializeField] private GameObject originObject;

    [Header("선택 되었을 때 색상")]
    [SerializeField] private Color selectedColor;
    [Header("해제 되었을 때 색상")]
    [SerializeField] private Color deselectedColor;

    private Image spriteImage;

    private bool isSelect = true;

    private void Awake()
    {
        InitChangeObjectComponent();
    }

    private void InitChangeObjectComponent()
    {
        spriteImage = changeObject.GetComponentInChildren<Image>();
    }

    public void SelectButton(Button button)
    {
        originObject.SetActive(!isSelect);
        changeObject.SetActive(isSelect);
        spriteImage.sprite = changeImage;

        SetButton(button, selectedColor);
    }

    public void DeSelectButton(Button button)
    {
        originObject.SetActive(isSelect);
        changeObject.SetActive(!isSelect);

        SetButton(button, deselectedColor);
    }

    private void SetButton(Button button, Color color)
    {
        ColorBlock buttonColors = button.colors;
        buttonColors.normalColor = color;
        buttonColors.highlightedColor = color;
        buttonColors.selectedColor = color;
        button.colors = buttonColors;
    }
}
