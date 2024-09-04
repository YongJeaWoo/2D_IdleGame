using UnityEngine;
using UnityEngine.UI;

public class ClickButtonComponent : MonoBehaviour
{
    [Header("연결된 패널 이름")]
    [SerializeField] protected string panelName;

    protected BottomDivisionComponent bottomDivision;
    protected Button myButton;
    protected ClickEffectButton effectButton;
    protected ExitButtonComponent exitButton;
    protected GameObject targetPanel;

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
        exitButton = GetComponent<ExitButtonComponent>();
        exitButton.SetBottomDivision(bottomDivision);
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

            targetPanel = null;

            foreach (var obj in objs)
            {
                if (obj.name == panelName)
                {
                    targetPanel = obj;
                    break;
                }
            }

            if (targetPanel != null)
            {
                exitButton.InitExitButton();

                bool isActive = !targetPanel.activeSelf;

                function.PanelOffButton(targetPanel);

                targetPanel.SetActive(isActive);

                if (isActive)
                {
                    effectButton.SelectButton(myButton);
                }
                else
                {
                    effectButton.DeSelectButton(myButton);
                }

                function.ActiveObjectKnifeUIObject();
            }
        }
    }

    public BottomDivisionComponent GetBottomDivisionComponent() => bottomDivision;
    public GameObject SetTargetPanel(GameObject obj) => targetPanel = obj;
    public GameObject GetTargetPanel() => targetPanel;
}
