using System.Numerics;

public class PlayerHealth : BaseHealth
{
    protected override void SetHp()
    {
        //TODO : ��ȭ �ɷ�ġ / ������ �ɷ�ġ �� ����Ͽ� MaxHp ����
        //�ӽ÷� SerializeField�� maxHp�� currentHp�� ����
        maxHp = BigInteger.Parse(maxHpString);
        currentHp = maxHp;
    }

    protected override void Death()
    {
        //TODO : ĳ���� ��� ó��
    }
}
