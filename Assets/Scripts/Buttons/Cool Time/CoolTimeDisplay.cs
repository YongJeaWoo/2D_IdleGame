using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CoolTimeDisplay : MonoBehaviour
{
    [Header("ÄðÅ¸ÀÓ")]
    [SerializeField] protected float coolTime;
    [Header("Äð Àû¿ë ÀÌ¹ÌÁö")]
    [SerializeField] protected Image coolImage;
    
    protected bool isCoolTime;

    protected FunctionBarComponent functionBar;

    protected ExplainableComponent explainableComponent;
    private Button button;

    protected virtual void Start()
    {
        FindRefer();
        InitButton();
    }

    private void FindRefer()
    {
        functionBar = UIManager.Instance.gameObject.GetComponentInChildren<FunctionBarComponent>();
        button = GetComponent<Button>();
        explainableComponent = GetComponent<ExplainableComponent>();
    }

    private void InitButton()
    {
        isCoolTime = false;
        coolImage.fillAmount = 0;
        button.onClick.AddListener(() =>
        {
            if (explainableComponent != null)
            {
                if (explainableComponent.GetIsClickActionAllowed())
                {
                    BehaviourButtonClick();
                }
            }
            else
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

    public virtual void BehaviourButtonClick()
    {
        if (isCoolTime) return;

        isCoolTime = true;
        StartCoroutine(CoolTime());
    }
}
