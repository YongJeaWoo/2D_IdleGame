using System.Numerics;
using UnityEngine;

public class SkillCollector : MonoBehaviour
{
    [Header("스킬 버튼 모음")]
    [SerializeField] private GameObject[] skillButtons;
    [Header("스킬 해제 기준")]
    [SerializeField] private int[] requiredAttackPoint = { 12, 30, 50, 60 , 80 };

    private void Start()
    {
        InitSkillSetting();
    }

    private void InitSkillSetting()
    {
        for (int i = 0; i< skillButtons.Length; i++)
        {
            skillButtons[i].SetActive(false);
        }

        PlayerManager.Instance.OnPlayerReady += CheckOpenSkill;
    }

    public void CheckOpenSkill()
    {
        BigInteger playerAttack = PlayerManager.Instance.GetAttack();

        for (int i = 0; i < skillButtons.Length; i++)
        {
            if (playerAttack >= requiredAttackPoint[i])
            {
                skillButtons[i].SetActive(true);
            }
            else
            {
                skillButtons[i].SetActive(false);
            }
        }
    }

    public GameObject[] GetSkillObjects() => skillButtons;
}
