using UnityEngine;

public class FunctionBarComponent : MonoBehaviour
{
    [Header("나이프 정보 바")]
    [SerializeField] private GameObject knifeCollectBar;
    [Header("기타 바들")]
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
