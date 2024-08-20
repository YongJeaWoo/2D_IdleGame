using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;

    [SerializeField] protected Vector2 direction;

    protected virtual void Move()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    private void Update()
    {
        Move();
    }
}
