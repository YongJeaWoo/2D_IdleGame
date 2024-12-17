using UnityEngine;
using UnityEngine.UI;

public class ClickButtonComponent : MonoBehaviour
{
    [Header("연결된 패널 이름")]
    [SerializeField] protected string panelName;

    protected readonly string DungeonSceneName = $"DungeonScene";
    protected readonly string DoNotFunctionPanel = $"Warning Panel";
    protected readonly string DoNotFunctionAlramText = $"현재 기능은 수행할 수 없습니다.";

    protected FunctionBarComponent functionBar;
    protected Button myButton;
    protected ClickEffectButton effectButton;
    protected GameObject targetPanel;

    protected ButtonCollector buttonCollector;

    protected virtual void Start()
    {
        InitButton();
        AddListenerButton();
    }

    protected virtual void OnDestroy()
    {
        RemoveListenerButton();
    }

    protected virtual void InitButton()
    {
        var UIObj = UIManager.Instance.gameObject;
        functionBar = UIObj.GetComponentInChildren<FunctionBarComponent>();
        myButton = GetComponent<Button>();
        effectButton = GetComponent<ClickEffectButton>();
        buttonCollector = GetComponentInParent<ButtonCollector>();
    }
    protected virtual void AddListenerButton()
    {
        myButton.onClick.AddListener(ClickButton);
    }
    protected virtual void RemoveListenerButton()
    {
        myButton.onClick.RemoveListener(ClickButton);
    }

    public virtual void ClickButton()
    {
        if (SceneStateManager.Instance.CurrentScene == DungeonSceneName)
        {
            var panel = PopupManager.Instance.InstantPopup(DoNotFunctionPanel);
            var warningPanel = panel.GetComponent<WarningPanel>();
            warningPanel.SetAlramPanelText(DoNotFunctionAlramText);
            return;
        }

        if (functionBar != null)
        {
            var objs = functionBar.GetOtherObjects();
            targetPanel = FindPanelByName(objs, panelName);

            if (targetPanel != null)
            {
                bool isActive = !targetPanel.activeSelf;

                functionBar.PanelOffButton(targetPanel);
                targetPanel.SetActive(isActive);

                if (isActive)
                {
                    buttonCollector.OnButtonSelected(this);
                    effectButton.SelectButton(myButton);
                }
                else
                {
                    DeselectButton();
                }

                functionBar.ActiveObjectKnifeUIObject();
            }
        }
    }

    public void DeselectButton()
    {
        effectButton.DeSelectButton(myButton);
    }

    private GameObject FindPanelByName(GameObject[] panels, string panelName)
    {
        foreach (var panel in panels)
        {
            if (panel.name == panelName)
            {
                return panel;
            }
        }
        return null;
    }

    public ClickEffectButton GetEffectButton() => effectButton;
}
