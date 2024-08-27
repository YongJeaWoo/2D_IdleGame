using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

public abstract class DropPossessItem : NumberFormatterText, IItemDropper
{
    protected abstract int PossessIndex { get; }

    protected void DropPossess(BigInteger amount)
    {
        var possess = UIManager.Instance.GetPossess();

        possess[PossessIndex] += amount;

        if (possess[PossessIndex] < BigInteger.Zero)
        {
            possess[PossessIndex] = BigInteger.Zero;
        }

        // �����õ� �ؽ�Ʈ ����
        string formattedAmount = FormatterText(possess[PossessIndex]);

        UIManager.Instance.UpdatePossessText(PossessIndex, formattedAmount); // ������ ��� �̺�Ʈ ȣ��
    }

    // ������ ���
    public abstract void DropItem();
}
