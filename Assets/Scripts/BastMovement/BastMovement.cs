using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BastMovement : MonoBehaviour
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
