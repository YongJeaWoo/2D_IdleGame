using UnityEngine;

public class TitleMoveBackground : MonoBehaviour
{
    [Header("이어질 배경")]
    [SerializeField] private Transform targetBG;
    [Header("이어질 두 배경 사이 거리")]
    [SerializeField] private float scrollAmount;
    [Header("이동 방향")]
    [SerializeField] private Vector3 moveDirection;

    [SerializeField] private float moveSpeed;

    private void Update()
    {
        MoveBackground();
    }

    private void MoveBackground()
    {
        transform.position += moveSpeed * Time.deltaTime * moveDirection;

        if (transform.position.x <= -scrollAmount)
        {
            transform.position = targetBG.position - moveDirection * scrollAmount;
        }
    }
}
