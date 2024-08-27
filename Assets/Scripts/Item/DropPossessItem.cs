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

        // 포맷팅된 텍스트 생성
        string formattedAmount = FormatterText(possess[PossessIndex]);

        UIManager.Instance.UpdatePossessText(PossessIndex, formattedAmount); // 아이템 드랍 이벤트 호출
    }

    // 아이템 드랍
    public abstract void DropItem();
}
