using UnityEngine;

public class KnifeNextData : MonoBehaviour
{
    [Header("���׷��̵� Į ������")]
    [SerializeField] private GameObject nextPrefab;

    public GameObject GetNextPrefab() => nextPrefab;
}
