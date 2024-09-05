using UnityEngine;
using UnityEngine.UI;

public class ClickEffectButton : MonoBehaviour
{
    [Header("���� �Ǿ��� �� ���� ǥ���� ����Ʈ")]
    [SerializeField] private Sprite changeImage;
    [Header("���� �Ǿ��� �� ǥ���� ������Ʈ")]
    [SerializeField] private GameObject changeObject;
    [Header("���� �Ǿ��� �� ǥ���� ������Ʈ")]
    [SerializeField] private GameObject originObject;

    [Header("���� �Ǿ��� �� ����")]
    [SerializeField] private Color selectedColor;
    [Header("���� �Ǿ��� �� ����")]
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
