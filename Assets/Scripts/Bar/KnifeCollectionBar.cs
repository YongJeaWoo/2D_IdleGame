using System.Collections.Generic;
using UnityEngine;

public class KnifeCollectionBar : MonoBehaviour
{
    private List<GameObject> attackKnifes = new List<GameObject>();

    private int currentCreatedCount = 0;
    private int createdMaxCount = 10;

    private bool isAutoPlay = false;
    public bool IsAutoPlay { get => isAutoPlay; set => isAutoPlay = value; }

    public void AddAttackKnifes(GameObject addKnife)
    {
        string trimName = addKnife.name.Replace("_UI(Clone)", "");
        addKnife.name = trimName;
        attackKnifes.Add(addKnife);
    }

    public List<GameObject> GetAttackKnifes() => attackKnifes;
    public int AddCreatedCount()
    {
        if (currentCreatedCount  > createdMaxCount)
        {
            currentCreatedCount = createdMaxCount;
            return currentCreatedCount;
        }

        return currentCreatedCount++;
    }
    public int GetCreatedCurrentCount() => currentCreatedCount;
    public int GetCreatedMaxCount() => createdMaxCount;
    public int UpgradeMaxCount() => createdMaxCount++;
}
