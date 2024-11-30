using System.Collections.Generic;
using UnityEngine;

public class KnifeData : MonoBehaviour
{
    [Header("UI Ä® ÇÁ¸®ÆÕ")]
    [SerializeField] private List<GameObject> knifes;

    public List<GameObject> GetUIKnifes() => knifes;
}
