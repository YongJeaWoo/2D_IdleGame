using System;
using System.Collections.Generic;
using UnityEngine;

public class KnifeCollectionBar : MonoBehaviour
{
    [Header("Ä® ÃÖ´ë »ý¼º °¹¼ö")]
    [SerializeField]private int createdMaxCount = 30;

    private List<GameObject> knifeList = new List<GameObject>();

    public static event Action OnUpdateKnife;

    public void AddAttackKnifes(GameObject addKnife)
    {
        knifeList.Add(addKnife);
        OnUpdateKnife?.Invoke();
    }

    public void RemoveAttackKnifes(GameObject removeKnife)
    {
        knifeList.Remove(removeKnife);
        OnUpdateKnife?.Invoke();
    }

    public List<GameObject> GetKnifesList() => knifeList;

    public int GetCreatedCurrentCount() => knifeList.Count;
    public int GetCreatedMaxCount() => createdMaxCount;
    public int UpgradeMaxCount() => createdMaxCount++;
}
