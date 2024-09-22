using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AutoMergeButton : AutoTextButton
{
    private FunctionBarComponent functionBar;
    private Coroutine autoMergeCoroutine;

    protected override void Start()
    {
        base.Start();
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
        var knifeCollectionBar = functionBar.GetKnifeCollectBar();
        var knifeList = knifeCollectionBar.GetKnifesList();

        if (knifeList.Count <= 1) return;

        List<GameObject> sortedKnifes = knifeList
            .OrderByDescending(k => k.GetComponent<KnifeAttack>().GetAttackPointString())
            .ToList();

        if (sortedKnifes.Count < 2) return;  

        bool merged = true;

        while (merged)
        {
            merged = false; 

            for (int i = 0; i < sortedKnifes.Count - 1; i++)
            {
                GameObject firstKnife = sortedKnifes[i];
                var firstData = firstKnife.GetComponent<KnifeNextData>();

                for (int j = i + 1; j < sortedKnifes.Count; j++)
                {
                    GameObject secondKnife = sortedKnifes[j];
                    var secondData = secondKnife.GetComponent<KnifeNextData>();

                    if (firstData != null && secondData != null && firstData.NextID == secondData.NextID)
                    {
                        var firstActivator = firstKnife.GetComponent<KnifeUIActivator>();
                        var secondActivator = secondKnife.GetComponent<KnifeUIActivator>();

                        if (firstActivator != null && secondActivator != null)
                        {
                            firstActivator.MergeObjects(secondKnife);

                            sortedKnifes.RemoveAt(j);
                            sortedKnifes.RemoveAt(i);

                            sortedKnifes = sortedKnifes
                                .OrderByDescending(k => k.GetComponent<KnifeAttack>().GetAttackPointString())
                                .ToList();

                            merged = true; 
                            break; 
                        }
                    }
                }

                if (merged) break; 
            }
        }
    }
}
