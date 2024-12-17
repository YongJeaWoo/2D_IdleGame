using UnityEngine;

public class BottomCollector : MonoBehaviour
{
    [SerializeField] private CreateKnifeButton button;

    private void OnEnable()
    {
        button.InitKnifeData();
    }
}
