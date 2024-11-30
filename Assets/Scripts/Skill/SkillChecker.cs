using UnityEngine;

public class SkillChecker : MonoBehaviour
{
    private FunctionBarComponent functionBar;
    private SkillCollector skillCollector;

    private void OnEnable()
    {
        DataCell.ClickButton += SkillOpenCheck;
    }

    private void OnDisable()
    {
        DataCell.ClickButton -= SkillOpenCheck;
    }

    private void Awake()
    {
        GetComponents();
    }

    private void GetComponents()
    {
        functionBar = GetComponent<FunctionBarComponent>();
        skillCollector = functionBar.GetSkillCollector();
    }

    private void SkillOpenCheck()
    {
        skillCollector.CheckOpenSkill();
    }
}
