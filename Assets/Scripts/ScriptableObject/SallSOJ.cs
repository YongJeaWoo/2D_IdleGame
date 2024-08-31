using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[CreateAssetMenu(fileName = "Sall", menuName = "Sall/StatEnhance")]
public class SallSOJ : ScriptableObject
{
    [SerializeField] protected string statName;
    [SerializeField] protected Sprite icon;

    public string StatName { get => statName; set => statName = value; }
    public Sprite Icon { get => icon; set => icon = value; }
}
