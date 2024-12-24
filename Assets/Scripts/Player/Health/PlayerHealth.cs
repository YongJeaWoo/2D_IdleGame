using System.Collections;
using System.Numerics;
using UnityEngine;

public class PlayerHealth : BaseHealth
{
    private bool isPlayerDead;

    protected override void Start()
    {
        base.Start();
        SetValues();
    }

    protected void OnEnable()
    {
        InitializeHealth();
    }

    protected override void SetValues()
    {
        myHealthBar = UIManager.Instance.GetHpBars()[0];
        myHealthText = UIManager.Instance.GetHpTexts()[0];
    }

    private void InitializeHealth()
    {
        maxHp = BigInteger.Parse(maxHpString);
        SetCurrentHpToMaxHp();
    }

    protected override void Death()
    {
        StartCoroutine(DeathCoroutine());
    }

    private IEnumerator DeathCoroutine()
    {
        if (isPlayerDead) yield break;
        isPlayerDead = true;

        var pAttack = GetComponent<PlayerAttack>();

        pAttack.enabled = false;
        anim.SetTrigger("isDead");
        anim.SetBool("isAttack", false);

        var panel = PopupManager.Instance.InstantPopup("Info Panel");
        var infoPanel = panel.GetComponent<InfoPanel>();
        infoPanel.SetInfoText("플레이어가 죽었습니다.");
        infoPanel.SetInsideInfoText("아무 키를 눌러 재시작");

        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        PopupManager.Instance.RemovePopup(panel.name);

        maxHp = BigInteger.Parse(maxHpString);
        SetCurrentHpToMaxHp();

        myHealthBar.fillAmount = (float)(double)currentHp / (float)(double)maxHp;

        EnemiesSpawnerSystem spawner = FindObjectOfType<EnemiesSpawnerSystem>();
        spawner.StopSpawningAndClearEnemies();

        LevelManager.Instance.LoadSaveRound();
        spawner.SpawnEnemies();

        isPlayerDead = false;

        pAttack.enabled = true;
    }

    public BigInteger SetMaxHp(BigInteger value)
    {
        maxHp = value;
        maxHpString = maxHp.ToString();
        return maxHp;
    }

    public bool GetIsPlayerDead() => isPlayerDead;
}
