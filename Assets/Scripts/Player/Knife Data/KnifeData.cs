using System.Collections.Generic;
using UnityEngine;

public class KnifeData : MonoBehaviour
{
    [Header("Į ������")]
    [SerializeField] private List<GameObject> knifes;

    public List<GameObject> GetUIKnifes() => knifes;
}
