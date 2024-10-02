using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class CoolTimeDisplay : MonoBehaviour
{
    [Header("ÄðÅ¸ÀÓ")]
    [SerializeField] protected float coolTime;
    [Header("Äð Àû¿ë ÀÌ¹ÌÁö")]
    [SerializeField] protected Image coolImage;
    protected Button button;
    protected bool isCoolTime;

    protected FunctionBarComponent functionBar;

    protected virtual void Start()
    {
        FindRefer();
        InitButton();
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
        button.onClick.AddListener(BehaviourButtonClick);
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
}
