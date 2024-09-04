using UnityEngine;

public class FunctionBarComponent : MonoBehaviour
{
    [Header("나이프 정보 바")]
    [SerializeField] private KnifeCollectionBar knifeCollectBar;
    [Header("기타 바들")]
    [SerializeField] private GameObject[] otherObjects;
    [Header("나이프 스크롤 뷰")]
    [SerializeField] private GameObject knifeScrollView;

    public void PanelOffButton(GameObject excludePanel = null)
    {
        foreach (var obj in otherObjects)
        {
            if (obj != excludePanel && obj.activeSelf)
            {
                obj.SetActive(false);
            }
        }
    }

    public void ActiveObjectKnifeUIObject()
    {
        bool isAnyPanelActive = false;

        foreach (var obj in otherObjects)
        {
            if (obj.activeSelf)
            {
                isAnyPanelActive = true;
                break;
            }
        }

        knifeScrollView.SetActive(!isAnyPanelActive);
    }

    public KnifeCollectionBar GetKnifeCollectBar()
    {
        knifeCollectBar.GetComponent<KnifeCollectionBar>();
        return knifeCollectBar;
    }
    public GameObject[] GetOtherObjects() => otherObjects;
}
