using System.Collections.Generic;
using UnityEngine;

public class KnifeData : MonoBehaviour
{
    [Header("UI�� Į ������")]
    [SerializeField] private List<GameObject> knifes;

    public List<GameObject> GetUIKnifes() => knifes;
}
