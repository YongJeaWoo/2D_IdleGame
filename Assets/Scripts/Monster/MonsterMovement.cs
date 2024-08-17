using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    
    private void Update()
    {
        transform.Translate(-transform.right * moveSpeed * Time.deltaTime);
    }
}
  