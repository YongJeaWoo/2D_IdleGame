using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Header("�̾��� ���")]
    [SerializeField] private Transform targetBG;
    [Header("�̾��� �� ��� ���� �Ÿ�")]
    [SerializeField] private float scrollAmount;
    [Header("��� ������ �ӵ�")]
    [SerializeField] private float moveSpeed;
    [Header("�̵� ����")]
    [SerializeField] private Vector3 moveDirection;

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
