using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExplainableComponent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject explainPanel;
    [SerializeField] private TextMeshProUGUI explainText;
    private string explainDetail;

    private bool isHolding = false;
    private float holdTimer = 0f;
    private float holdTime = 0.5f;
    private bool isExplainActive = false;
    private bool isClickActionAllowed = false;

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

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (explainPanel == null) return;

        explainText.text = explainDetail;
        isHolding = true;
        isClickActionAllowed = true;
        holdTimer = 0;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (explainPanel == null) return;

        explainText.text = string.Empty;
        isHolding = false;

        if (isExplainActive)
        {
            isClickActionAllowed = false;
            explainPanel.SetActive(false);
            isExplainActive = false;
        }

        holdTimer = 0;
    }

    public string SetExplainDetail(string explain) => explainDetail = explain;
    public bool GetIsClickActionAllowed() => isClickActionAllowed;
}