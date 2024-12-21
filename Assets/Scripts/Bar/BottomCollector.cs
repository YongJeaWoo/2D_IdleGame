using UnityEngine;

public class BottomCollector : MonoBehaviour
{
    [SerializeField] private CreateKnifeButton button;

    public void Initialize(PlayerManager playerManager)
    {
        button.InitKnifeData(playerManager);
        button.InitPools();
    }
}
