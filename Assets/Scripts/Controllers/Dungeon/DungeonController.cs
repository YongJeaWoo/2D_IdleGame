using UnityEngine;

public class DungeonController : MonoBehaviour
{
    private void Start()
    {
        DungeonDataManager.Instance.StartDungeonTime();
    }
}
