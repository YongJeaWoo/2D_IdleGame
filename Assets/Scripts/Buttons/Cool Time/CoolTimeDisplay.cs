using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using TMPro;

public abstract class CoolTimeDisplay : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("설명 패널")]
    [SerializeField] protected GameObject explainPanel;
    [SerializeField] protected TextMeshProUGUI explainText;
    protected string explainDetail;

    protected bool isHolding = false;
    protected float holdTimer = 0f;
    protected float holdTime = 0.5f;
    protected bool isExplainActive = false;
    protected bool isClickActionAllowed = false;

    [Header("쿨타임")]
    [SerializeField] protected float coolTime;
    [Header("쿨 적용 이미지")]
    [SerializeField] protected Image coolImage;
    protected Button button;
    protected bool isCoolTime;

    protected FunctionBarComponent functionBar;

    protected virtual void Start()
    {
        FindRefer();
        InitButton();
    }

    protected virtual void Update()
    {
        HoldButton();
    }

    protected virtual void HoldButton()
    {
        if (isHolding)
        {
            holdTimer += Time.deltaTime;

            if (holdTimer >= holdTime && !isExplainActive)
            {
                explainPanel.SetActive(true);
                isExplainActive = true;
                isClickActionAllowed = false;
            }
        }
    }

    private void FindRefer()
    {
        functionBar = UIManager.Instance.gameObject.GetComponentInChildren<FunctionBarComponent>();
    }

    protected virtual void InitButton()
    {
        isCoolTime = false;
        coolImage.fillAmount = 0;

        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if (isClickActionAllowed)
            {
                BehaviourButtonClick();
            }
        });
    }

    protected IEnumerator CoolTime()
    {
        coolImage.fillAmount = 1;

        float elapsed = 0f;
        while (elapsed < coolTime)
        {
            elapsed += Time.deltaTime;
            coolImage.fillAmount = 1 - (elapsed / coolTime);
            yield return null;
        }

        coolImage.fillAmount = 0;
        isCoolTime = false;
    }

    public abstract void BehaviourButtonClick();

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (explainPanel == null) return;

        isHolding = true;
        isClickActionAllowed = true;
        holdTimer = 0;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (explainPanel == null) return;

        isHolding = false;

        if (isExplainActive)
        {
            isClickActionAllowed = false;
            explainPanel.SetActive(false);
            isExplainActive = false;
        }

        holdTimer = 0;
    }
}
