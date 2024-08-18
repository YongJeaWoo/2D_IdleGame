using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public GameObject GetPlayer() => player;
}
