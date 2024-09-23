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
                        StartCoroutine(MoveToCenterAndMerge(firstKnife, secondKnife));

                        sortedKnifes.RemoveAt(j);
                        sortedKnifes.RemoveAt(i);

                        sortedKnifes = sortedKnifes
                            .OrderByDescending(k => k.GetComponent<KnifeAttack>().GetAttackPointString())
                            .ToList();

                        merged = true; 
                        break; 
                    }
                }

                if (merged) break; 
            }
        }
    }

    private IEnumerator MoveToCenterAndMerge(GameObject firstKnife, GameObject secondKnife)
    {
        Vector3 firstPos = firstKnife.transform.position;
        Vector3 secondPos = secondKnife.transform.position;
        Vector3 middlePos = (firstPos + secondPos) / 2;

        float duration = 0.8f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            float easeOut = Mathf.Pow(t, 0.5f);

            firstKnife.transform.position = Vector3.Lerp(firstPos, middlePos, easeOut);
            secondKnife.transform.position = Vector3.Lerp(secondPos, middlePos, easeOut);

            yield return null;
        }

        var firstActivator = firstKnife.GetComponent<KnifeUIActivator>();

        firstActivator.MergeObjects(secondKnife);
    }
}
