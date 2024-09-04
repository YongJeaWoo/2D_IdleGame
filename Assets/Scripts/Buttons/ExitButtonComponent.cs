using UnityEngine;
using UnityEngine.UI;

public class ExitButtonComponent : MonoBehaviour
{
    private GameObject targetPanel;
    private Button exitButton;
    private ClickButtonComponent clickButton;
    private ClickEffectButton effectButton;
    private BottomDivisionComponent bottomDivision;

    public void InitExitButton()
    {
        clickButton = GetComponent<ClickButtonComponent>();
        effectButton = GetComponent<ClickEffectButton>();
        targetPanel = clickButton.GetTargetPanel();
        exitButton = targetPanel.transform.GetChild(0).GetComponent<Button>();
        exitButton.onClick.AddListener(ClosePanel);
    }

    private void OnDisable()
    {
        exitButton.onClick.RemoveListener(ClosePanel);
    }

    private void ClosePanel()
    {
        if (clickButton != null && clickButton.GetTargetPanel() != null)
        {
            effectButton.DeSelectButton(clickButton.gameObject.GetComponent<Button>());
            targetPanel.SetActive(false);
            bottomDivision.GetFunctionBar().ActiveObjectKnifeUIObject();
            clickButton.SetTargetPanel(null);
        }
    }

    public BottomDivisionComponent SetBottomDivision(BottomDivisionComponent division) => bottomDivision = division;
}
