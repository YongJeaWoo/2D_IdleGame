using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string dropGoldText;
    private BigInteger gold;

    private void OnEnable()
    {
        gold = BigInteger.Parse(dropGoldText);
    }

    public void DropPossess(int possessIndex, BigInteger amount)
    {
        // 인덱스 범위 체크
        if (possessIndex < 0 || possessIndex >= UIManager.Instance.GetPossess().Length)
        {
            Debug.LogError("잘못된 재화 인덱스입니다.");
            return;
        }

        // 재화 증가
        UIManager.Instance.GetPossess()[possessIndex] += amount;

        // 값이 0 미만으로 내려가지 않도록 제한
        if (UIManager.Instance.GetPossess()[possessIndex] < BigInteger.Zero)
        {
            UIManager.Instance.GetPossess()[possessIndex] = BigInteger.Zero;
        }

        // UI 업데이트
        UIManager.Instance.UpdatePossessText();
    }

    // 아이템 드랍
    public virtual void Drop()
    {
        DropPossess(0, gold);
    }
}
