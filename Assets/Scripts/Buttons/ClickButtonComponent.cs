using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClickButtonComponent : MonoBehaviour
{
    [Header("연결된 패널 이름")]
    [SerializeField] protected string panelName;

    protected BottomDivisionComponent bottomDivision;
    protected Button myButton;

    protected ClickEffectButton effectButton;

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

            GameObject isPanel = null;

            foreach (var obj in objs)
            {
                if (obj.name == panelName)
                {
                    isPanel = obj;
                    break;
                }
            }

            if (isPanel != null)
            {
                bool isActive = !isPanel.activeSelf;

                foreach (var obj in objs)
                {
                    if (obj != isPanel && obj.activeSelf)
                    {
                        obj.SetActive(false);
                    }
                }

                isPanel.SetActive(isActive);

                if (isActive)
                {
                    effectButton.SelectButton(myButton);
                }
                else
                {
                    effectButton.DeSelectButton(myButton);
                }
            }
        }
    }
}
