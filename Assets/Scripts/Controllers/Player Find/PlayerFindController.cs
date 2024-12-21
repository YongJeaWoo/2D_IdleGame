using UnityEngine;

public class PlayerFindController : MonoBehaviour
{
    private void Start()
    {
        PlayerManager.Instance.FindPlayer();
    }
}
