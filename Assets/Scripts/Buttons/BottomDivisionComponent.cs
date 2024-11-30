using UnityEngine;

public class BottomDivisionComponent : MonoBehaviour
{
    [Header("기능 바 모음")]
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
