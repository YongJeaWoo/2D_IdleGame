using UnityEngine;

public class DisplayRatioObject : MonoBehaviour
{
    [SerializeField] private Vector2 screenPositionRatio;

    private void Start()
    {
        //InitObject();
    }

    // TODO : 오브젝트 위치를 실시간 테스트하기 위함 나중에 제거
    private void Update()
    {
        InitObject();
    }

    private void InitObject()
    {
        Vector3 screenPosition = new Vector3(screenPositionRatio.x * Screen.width, screenPositionRatio.y * Screen.height, 0);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0;

        transform.position = worldPosition;
    }
}
