using UnityEngine;

public class KnifeMovement : BaseMovement
{
    private void Start()
    {
        InitDir();
    }

    private void InitDir()
    {
        transform.rotation = Quaternion.Euler(0, 0, 270);
        moveSpeed = 2.5f;
        direction = new(0, 1);
    }
}
