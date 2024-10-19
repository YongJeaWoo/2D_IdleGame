using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class SkillEnhanceBar : MonoBehaviour
{
    private SkillCollector skillCollector;

    [Header("스킬 강화 셀들")]
    [SerializeField] private GameObject[] skillCells;

    private void Start()
    {
        ApplySystem();
    }

    private void OnEnable()
    {
        DataCell.ClickButton += OpenSkill;
    }

    private void OnDisable()
    {
        DataCell.ClickButton -= OpenSkill;
    }

    private void ApplySystem()
    {
        var barComponent = transform.parent.GetComponent<FunctionBarComponent>();
        skillCollector = barComponent.GetSkillCollector();

        skillCollector.InitSkillSetting();  
    }

    public void OpenSkill()
    {
        skillCollector.CheckOpenSkill();
    }
}
