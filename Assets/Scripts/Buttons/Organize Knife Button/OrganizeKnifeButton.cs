using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OrganizeKnifeButton : MonoBehaviour
{
    [Header("ÄðÅ¸ÀÓ")]
    [SerializeField] private float coolTime;
    [Header("Äð Àû¿ë ÀÌ¹ÌÁö")]
    [SerializeField] private Image coolImage;

    private FunctionBarComponent functionBar;
    private Button button;
    private Coroutine organizeCoroutine;
    private bool isCoolTime;

    private void Awake()
    {
        FindRefer();
        InitButton();
    }

    private void FindRefer()
    {
        var grandParent = transform.parent.parent.parent;
        functionBar = grandParent.GetComponent<FunctionBarComponent>();
    }

    private void InitButton()
    {
        isCoolTime = false;
        coolImage.fillAmount = 0;

        button = GetComponent<Button>();
        button.onClick.AddListener(OrganizeButtonClick);
    }

    public void OrganizeButtonClick()
    {
        if (isCoolTime) return;

        if (organizeCoroutine == null)
        {
            organizeCoroutine = StartCoroutine(OrganizeKnifeCoroutine());
        }
    }

    private IEnumerator OrganizeKnifeCoroutine()
    {
        isCoolTime = true;
        OrganizeBehaviour();
        yield return StartCoroutine(CoolTime());
        organizeCoroutine = null;
    }

    private IEnumerator CoolTime() 
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

    private void OrganizeBehaviour() 
    {
        var scrollView = functionBar.GetKnifeScrollView();
        var content = scrollView.transform.GetChild(0).GetChild(0);
        RectTransform contentRect = content.GetComponent<RectTransform>();

        float contentWidth = contentRect.rect.width;
        float contentHeight = contentRect.rect.height;

        int childCount = content.childCount;

        float spacing = 0.5f;

        int columnCount = Mathf.FloorToInt(contentWidth / (50 + spacing));
        if (columnCount == 0) columnCount = 1;

        float cellWidth = (contentWidth - (columnCount - 1) * spacing) / columnCount;
        float cellHeight = cellWidth + 70;

        List<Transform> sortedKnifes = content.Cast<Transform>().OrderByDescending(c => c.GetComponent<KnifeAttack>().GetAttackPoint()).ToList();

        float startX = -contentWidth / 2 + cellWidth / 2;
        float startY = contentHeight / 2 - cellHeight / 2;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = sortedKnifes[i];

            int row = i / columnCount;
            int column = i % columnCount;

            float xPos = startX + column * (cellWidth + spacing);
            float yPos = startY - row * (cellHeight + spacing); 

            child.localPosition = new Vector3(xPos, yPos, 0);
        }
    }
}
