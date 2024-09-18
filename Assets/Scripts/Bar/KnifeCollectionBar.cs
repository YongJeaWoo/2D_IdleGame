using System;
using System.Collections.Generic;
using UnityEngine;

public class KnifeCollectionBar : MonoBehaviour
{
    [Header("Ä® ÃÖ´ë »ý¼º °¹¼ö")]
    [SerializeField]private int createdMaxCount = 30;

    private List<GameObject> attackKnifes = new List<GameObject>();

    public static event Action OnUpdateKnife;

    public void AddAttackKnifes(GameObject addKnife)
    {
        string trimName = addKnife.name.Replace("(Clone)", "");
        addKnife.name = trimName;
        attackKnifes.Add(addKnife);
        OnUpdateKnife?.Invoke();
    }

    public void RemoveAttackKnifes(GameObject removeKnife)
    {
        attackKnifes.Remove(removeKnife);
        OnUpdateKnife?.Invoke();
    }

    public List<GameObject> GetAttackKnifes() => attackKnifes;

    public int GetCreatedCurrentCount() => attackKnifes.Count;
    public int GetCreatedMaxCount() => createdMaxCount;
    public int UpgradeMaxCount() => createdMaxCount++;
}
