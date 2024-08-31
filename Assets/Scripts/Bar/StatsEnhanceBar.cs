using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class StatsEnhanceBar : EnhanceBar
{
    [SerializeField] private GameObject[] stats;
    [SerializeField] private SallSOJ[] sallSOJ;

    private void OnEnable()
    {
        InitSOJData();
        Cell.OnEnhance += UpdateData;
    }

    private void InitSOJData()
    {
        for(int i = 0; i < sallSOJ.Length; i++)
        {
            var button = stats[i].GetComponent<Cell>();
            button.InitSall(sallSOJ[i]);
        }
    }

    private void UpdateData()
    {
        for (int i = 0; i < sallSOJ.Length; i++)
        {
            var button = stats[i].GetComponent<Cell>();
            button.UpdateInfo();
        }
    }

    private void OnDisable()
    {
        Cell.OnEnhance -= InitSOJData;
    }
}
