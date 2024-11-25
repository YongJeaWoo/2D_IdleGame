using UnityEngine;

public class KnifeNextData : MonoBehaviour
{
    [Header("업그레이드 ID")]
    [SerializeField] private int nextID;
    public int NextID { get => nextID; set => nextID = value; }

    private int maxLevel = 20;

    public int GetNextID(int currentID)
    {
        return currentID + 1;
    }

    public bool IsAtMaxLevelID(int currentID)
    {
        return currentID >= maxLevel;
    }
}
