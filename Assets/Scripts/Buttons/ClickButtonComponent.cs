using UnityEngine;
using UnityEngine.UI;

public class ClickButtonComponent : MonoBehaviour
{
    [Header("연결된 패널 이름")]
    [SerializeField] protected string panelName;

    protected BottomDivisionComponent bottomDivision;
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
        var initParentObj = transform.parent.parent.parent.gameObject;
        bottomDivision = initParentObj.GetComponent<BottomDivisionComponent>();
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
        var function = bottomDivision.GetFunctionBar();

        if (function != null)
        {
            var objs = function.GetOtherObjects();
            targetPanel = FindPanelByName(objs, panelName);

            if (targetPanel != null)
            {
                bool isActive = !targetPanel.activeSelf;

                function.PanelOffButton(targetPanel);

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

                function.ActiveObjectKnifeUIObject();
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

    public BottomDivisionComponent GetBottomDivisionComponent() => bottomDivision;
    public GameObject SetTargetPanel(GameObject obj) => targetPanel = obj;
    public GameObject GetTargetPanel() => targetPanel;
}
