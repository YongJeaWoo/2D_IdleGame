using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommonPanel : MonoBehaviour
{
    protected readonly string IsOpen = $"isOpen";

    [SerializeField] protected GameObject myPanel;
    [SerializeField] protected Button exitButton;
    [SerializeField] protected Button confirmButton;

    protected Animator animator;

    protected bool isOpen = true;

    protected virtual void Start()
    {
        InitPanel();
    }

    protected virtual void InitPanel()
    {
        animator = GetComponent<Animator>();
        confirmButton.onClick.AddListener(ConfirmGame);
        animator.SetBool(IsOpen, isOpen);
    }

    public void OffSetting(BaseEventData eventData)
    {
        PointerEventData pointerEventData = eventData as PointerEventData;
        bool outSidePanelSize = !RectTransformUtility.RectangleContainsScreenPoint(
    myPanel.GetComponent<RectTransform>(),
    pointerEventData.position,
    Camera.main);

        if (myPanel != null && outSidePanelSize)
        {
            StartCoroutine(RemovePopupCoroutine());
        }
    }

    protected IEnumerator RemovePopupCoroutine()
    {
        if (animator != null)
        {
            animator.SetBool(IsOpen, !isOpen);

            while (!IsAnimatorFinished(animator, "Close"))
            {
                yield return null;
            }
        }

        var parentName = transform.parent.name;
        PopupManager.Instance.RemovePopup(parentName);
    }

    protected bool IsAnimatorFinished(Animator animator, string name)
    {
        var aniInfo = animator.GetCurrentAnimatorStateInfo(0);
        return aniInfo.IsName(name) && aniInfo.normalizedTime >= 1f;
    }

    public void ConfirmGame()
    {
        StartCoroutine(RemovePopupCoroutine());
    }
}
