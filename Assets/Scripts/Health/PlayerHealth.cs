using System.Numerics;

public class PlayerHealth : BaseHealth
{
    protected override void SetHp()
    {
        //TODO : 강화 능력치 / 아이템 능력치 등 계산하여 MaxHp 설정
        //임시로 SerializeField된 maxHp를 currentHp로 설정
    }

    protected override void Death()
    {
        //TODO : 캐릭터 사망 처리
    }
}
