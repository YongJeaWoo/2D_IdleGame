using System.Collections;
using UnityEngine;

public class MonsterMovement : BaseMovement
{
    [Header("이동 시간")]
    [SerializeField] private float moveDuration;

    private Vector2 targetPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            targetPosition = GetScreenRightEdgePosition();
            StartCoroutine(MoveToScreenRight(targetPosition, moveDuration));
        }
    }

    private Vector2 GetScreenRightEdgePosition()
    {
        Vector2 screenRightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, Camera.main.transform.position.z));
        Vector2 monsterPosition = transform.position;

        return new Vector2(screenRightEdge.x, monsterPosition.y);
    }

    private IEnumerator MoveToScreenRight(Vector2 target, float duration)
    {
        float elapsedTime = 0f;
        Vector2 startingPos = transform.position;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.position = Vector2.Lerp(startingPos, target, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
    }
}
  