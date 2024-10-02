using UnityEngine;

public class BottomDivisionComponent : MonoBehaviour
{
    [Header("��� �� ����")]
    [SerializeField] private FunctionBarComponent functionBar;

    private void Awake()
    {
        InitBar();
    }

    private void InitBar()
    {
        functionBar.GetKnifeCollectBar().gameObject.SetActive(true);
    }
}
