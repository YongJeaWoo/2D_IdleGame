using UnityEngine;

public class FunctionBarComponent : MonoBehaviour
{
    [Header("������ ���� ��")]
    [SerializeField] private GameObject knifeCollectBar;
    [Header("��Ÿ �ٵ�")]
    [SerializeField] private GameObject[] otherObjects;

    public void PanelOffButton()
    {
        foreach (var obj in otherObjects)
        {
            if (obj.activeSelf)
            {
                obj.SetActive(false);
            }
        }
    }

    public GameObject GetKnifeInfoBar() => knifeCollectBar;
    public GameObject[] GetOtherObjects() => otherObjects;
}
