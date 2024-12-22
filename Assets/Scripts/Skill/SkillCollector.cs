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

        PlayerManager.Instance.OnPlayerReady += () =>
        {
            if (PlayerManager.Instance.GetPlayer() != null)
            {
                CheckOpenSkill();
            }
        };
    }

    public void CheckOpenSkill()
    {
        BigInteger playerAttack = PlayerManager.Instance.GetAttack();

        if (skillButtons.Length != requiredAttackPoint.Length)
        {
            Debug.LogError("스킬 버튼이 아직 활성화 되지 않음");
            return;
        }

        for (int i = 0; i < skillButtons.Length; i++)
        {
            if (skillButtons[i] == null)
            {
                continue; 
            }

            try
            {
                skillButtons[i].SetActive(playerAttack >= requiredAttackPoint[i]);
            }
            catch (MissingReferenceException ex)
            {
                Debug.LogError($"스킬 버튼의 {i} 부분이 Missing 에러가 남: {ex.Message}");
            }
        }
    }

    public GameObject[] GetSkillObjects() => skillButtons;
}
