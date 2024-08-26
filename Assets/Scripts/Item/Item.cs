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
        // �ε��� ���� üũ
        if (possessIndex < 0 || possessIndex >= UIManager.Instance.GetPossess().Length)
        {
            Debug.LogError("�߸��� ��ȭ �ε����Դϴ�.");
            return;
        }

        // ��ȭ ����
        UIManager.Instance.GetPossess()[possessIndex] += amount;

        // ���� 0 �̸����� �������� �ʵ��� ����
        if (UIManager.Instance.GetPossess()[possessIndex] < BigInteger.Zero)
        {
            UIManager.Instance.GetPossess()[possessIndex] = BigInteger.Zero;
        }

        // UI ������Ʈ
        UIManager.Instance.UpdatePossessText();
    }

    // ������ ���
    public virtual void Drop()
    {
        DropPossess(0, gold);
    }
}
