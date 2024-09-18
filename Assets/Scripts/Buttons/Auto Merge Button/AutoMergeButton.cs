using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AutoMergeButton : AutoTextButton
{
    private FunctionBarComponent functionBar;
    private Coroutine autoMergeCoroutine;

    protected override void Awake()
    {
        base.Awake();
        InitFunction();
    }

    private void InitFunction()
    {
        functionBar = UIManager.Instance.gameObject.GetComponentInChildren<FunctionBarComponent>();
    }

    public override void ToggleActiveButton()
    {
        base.ToggleActiveButton();

        if (isAutoPlay)
        {
            autoMergeCoroutine = StartCoroutine(AutoMergeCoroutine());
        }
        else
        {
            if (autoMergeCoroutine != null)
            {
                StopCoroutine(autoMergeCoroutine);
                autoMergeCoroutine = null;
            }
        }
    }

    private IEnumerator AutoMergeCoroutine()
    {
        while (isAutoPlay)
        {
            AutoMerging();
            yield return new WaitForSeconds(2f);
        }
    }

    public void AutoMerging()
    {
        var scrollView = functionBar.GetKnifeScrollView();
        var content = scrollView.transform.GetChild(0).GetChild(0);
        RectTransform contentRect = content.GetComponent<RectTransform>();

        List<Transform> sortedKnifes = content.Cast<Transform>()
            .OrderByDescending(c => c.GetComponent<KnifeAttack>().GetAttackPoint())
            .ToList();

        if (sortedKnifes.Count == 0) return;

        for (int i = 0; i < sortedKnifes.Count - 1; i++)
        {
            Transform firstKnife = sortedKnifes[i];

            for (int j = i + 1; j < sortedKnifes.Count; j++)
            {
                Transform secondKnife = sortedKnifes[j];

                if (firstKnife.name == secondKnife.name)
                {
                    var firstActivator = firstKnife.GetComponent<KnifeUIActivator>();
                    var secondActivator = secondKnife.GetComponent<KnifeUIActivator>();

                    if (firstActivator != null && secondActivator != null)
                    {
                        firstActivator.MergeObjects(secondKnife.gameObject);

                        sortedKnifes.RemoveAt(j);
                        sortedKnifes.RemoveAt(i);

                        i = -1;  
                        break;
                    }
                }
            }
        }
    }
}
