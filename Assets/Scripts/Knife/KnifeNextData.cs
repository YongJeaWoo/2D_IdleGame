using UnityEngine;

public class KnifeNextData : MonoBehaviour
{
    [Header("업그레이드 ID")]
    [SerializeField] private int nextID;
    public int NextID { get => nextID; set => nextID = value; }

    public int GetNextID(int currentID)
    {
        return currentID + 1;
    }
}
