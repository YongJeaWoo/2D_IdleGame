using System.Collections;
using TMPro;
using UnityEngine;

public class AlramPanel : MonoBehaviour
{
    [Header("알림 텍스트")]
    [SerializeField] protected TextMeshProUGUI infoText;

    protected Animator animator;
    protected string openText = $"isOpen";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        StartCoroutine(AutoPanel(this));
    }

    protected IEnumerator AutoPanel(AlramPanel panel)
    {
        animator.SetBool(openText, true);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        yield return new WaitForSecondsRealtime(1.8f);

        animator.SetBool(openText, false);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && animator.GetCurrentAnimatorStateInfo(0).IsName("Close"));

        PopupManager.Instance.RemovePopup(panel.name);
    }
}
