using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;

    [SerializeField] protected Vector2 direction;

    protected virtual void Move()
    {
        transform.Translate(moveSpeed * Time.deltaTime * direction);
    }

    private void Update()
    {
        Move();
    }
}
