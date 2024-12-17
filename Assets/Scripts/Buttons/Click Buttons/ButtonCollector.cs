using UnityEngine;

public class ButtonCollector : MonoBehaviour
{
    private ClickButtonComponent currentSelectedButton;

    public void OnButtonSelected(ClickButtonComponent button)
    {
        if (currentSelectedButton != null && currentSelectedButton != button)
        {
            currentSelectedButton.DeselectButton();
        }

        currentSelectedButton = button;
    }
}
