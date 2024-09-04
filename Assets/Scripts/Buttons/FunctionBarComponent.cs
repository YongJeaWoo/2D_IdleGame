using UnityEngine;

public class FunctionBarComponent : MonoBehaviour
{
    [Header("������ ���� ��")]
    [SerializeField] private KnifeCollectionBar knifeCollectBar;
    [Header("��Ÿ �ٵ�")]
    [SerializeField] private GameObject[] otherObjects;
    [Header("������ ��ũ�� ��")]
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
