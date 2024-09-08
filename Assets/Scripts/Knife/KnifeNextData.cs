using UnityEngine;

public class KnifeNextData : MonoBehaviour
{
    [Header("¾÷±×·¹ÀÌµå Ä® ÇÁ¸®ÆÕ")]
    [SerializeField] private GameObject nextPrefab;

    public GameObject GetNextPrefab() => nextPrefab;
}
