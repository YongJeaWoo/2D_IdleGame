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
    private static ClickButtonComponent lastSelectButtonComponent = null;

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
                    if (lastSelectButtonComponent != null && lastSelectButtonComponent != this)
                    {
                        lastSelectButtonComponent.effectButton.DeSelectButton(lastSelectButtonComponent.myButton);
                    }

                    effectButton.SelectButton(myButton);

                    lastSelectButtonComponent = this;
                }
                else
                {
                    effectButton.DeSelectButton(myButton);
                    lastSelectButtonComponent = null;
                }

                functionBar.ActiveObjectKnifeUIObject();
            }
        }
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

    public GameObject SetTargetPanel(GameObject obj) => targetPanel = obj;
    public GameObject GetTargetPanel() => targetPanel;
}
