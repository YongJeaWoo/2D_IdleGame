using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OrganizeKnifeButton : CoolTimeDisplay
{
    private Coroutine organizeCoroutine;
    private Transform content;
    private GridLayoutGroup gridLayoutGroup;

    protected override void Start()
    {
        base.Start();
        FindContent();
    }

    private void FindContent()
    {
        var scrollView = functionBar.GetKnifeScrollView();
        content = scrollView.transform.GetChild(0).GetChild(0);
        gridLayoutGroup = content.GetComponent<GridLayoutGroup>();
        gridLayoutGroup.enabled = false;
    }

    public override void BehaviourButtonClick()
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


    private void OrganizeBehaviour()
    {
        var knifeCollectionBar = functionBar.GetKnifeCollectBar();
        var knifeList = knifeCollectionBar.GetKnifesList();

        gridLayoutGroup.enabled = true;

        knifeList = knifeList
            .Where(knife => knife.TryGetComponent<KnifeAttack>(out var knifeAttack))
            .OrderByDescending(knife => knife.GetComponent<KnifeAttack>().GetAttackPoint()) 
            .ToList();

        for (int i = 0; i < knifeList.Count; i++)
        {
            var knife = knifeList[i];
            var rectTransform = knife.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.SetSiblingIndex(i); 
            }
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(gridLayoutGroup.GetComponent<RectTransform>());

        gridLayoutGroup.enabled = false;
    }
}
