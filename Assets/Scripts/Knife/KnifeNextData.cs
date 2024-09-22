using UnityEngine;

public class KnifeNextData : MonoBehaviour
{
    [Header("���׷��̵� ID")]
    [SerializeField] private int nextID;
    public int NextID { get => nextID; set => nextID = value; }

    public int GetNextID(int currentID)
    {
        return currentID + 1;
    }
}
