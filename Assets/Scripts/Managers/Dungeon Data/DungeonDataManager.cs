using SingletonBase.DontDestroySingleton;
using System.Collections;
using TMPro;
using UnityEngine;

public class DungeonDataManager : SingletonBase<DungeonDataManager>
{
    private readonly string dungeonEnd = $"Info Panel";

    [SerializeField] private DungeonData currentDungeonData;
    private TextMeshProUGUI remaingTimerText;

    private float remaingTimer;

    private Coroutine blinkCoroutine;
    private WaitForSeconds blinkTimer = new(0.5f);

    public void SetDungeonData(DungeonData data)
    {
        currentDungeonData = data;
        ObjectPoolManager.Instance.InitObjectPool(currentDungeonData.dungeonObj);
    }

    private void UpdateDungeonInfo()
    {
        if (currentDungeonData == null) return;

        var uiManager = UIManager.Instance;
        var roundText = uiManager.GetDungeonText();
        roundText.gameObject.SetActive(true);
        remaingTimerText = uiManager.GetRoundText();

        if (roundText != null)
        {
            roundText.text = currentDungeonData.dungeonName;
        }
    }

    public void StartDungeonTime()
    {
        UpdateDungeonInfo();
        remaingTimer = currentDungeonData.dungeonTimer;
        var obj = ObjectPoolManager.Instance.GetToPool(currentDungeonData.dungeonObj);
        obj.transform.position = new Vector3(2f, 1.25f, 0);
        obj.name = currentDungeonData.objectName;

        switch (currentDungeonData.dungeonType)
        {
            case E_DungeonType.Coin:
                {
                    obj.AddComponent<DungeonGoldItem>();
                    break;
                }
            case E_DungeonType.Ore:
                {
                    obj.AddComponent<DungeonOreItem>();
                    break;
                }
            case E_DungeonType.Capsule:
                {
                    obj.AddComponent<DungeonCapsuleItem>();
                    break;
                }
        }

        var spriteRender = obj.GetComponent<SpriteRenderer>();
        spriteRender.sprite = currentDungeonData.dungeonObjSprite;

        UpdateTimerUI();
        StartCoroutine(EnterDungeonTimeLimitCoroutine());
    }

    private IEnumerator EnterDungeonTimeLimitCoroutine()
    {
        yield return new WaitForSeconds(1.0f);

        while (remaingTimer > 0)
        {
            remaingTimer -= Time.deltaTime;
            UpdateTimerUI();

            yield return null;
        }

        DungeonEnd();
    }

    private void UpdateTimerUI()
    {
        if (remaingTimerText != null)
        {
            if (remaingTimer <= 0)
            {
                remaingTimerText.text = $"00:00";

                if (blinkCoroutine != null)
                {
                    StopCoroutine(blinkCoroutine);
                    remaingTimerText.color = Color.white;
                }
            }
            else
            {
                int min = Mathf.FloorToInt(remaingTimer / 60f);
                int sec = Mathf.FloorToInt(remaingTimer % 60f);

                remaingTimerText.text = $"{min}:{sec:D2}";

                if (remaingTimer <= 10)
                {
                    if (blinkCoroutine == null)
                    {
                        blinkCoroutine = StartCoroutine(BlinkTextCoroutine());
                    }
                }
                else
                {
                    if (blinkCoroutine != null)
                    {
                        StopCoroutine(blinkCoroutine);
                        blinkCoroutine = null;
                    }

                    remaingTimerText.color = Color.white;
                }
            }
        }
    }

    private IEnumerator BlinkTextCoroutine()
    {
        bool isRed = true;

        while (remaingTimer <= 10 && remaingTimer > 0)
        {
            remaingTimerText.color = isRed ? Color.red : Color.white;
            isRed = !isRed;
            yield return blinkTimer;
        }

        remaingTimerText.color = Color.white;
    }

    private void DungeonEnd()
    {
        StopAllCoroutines();
        remaingTimer = 0;
        UpdateTimerUI();

        var playerAttack = PlayerManager.Instance.GetPlayer().GetComponent<PlayerAttack>();
        playerAttack.StopAttack();

        var dungeonText = UIManager.Instance.GetDungeonText();
        dungeonText.gameObject.SetActive(false);

        StartCoroutine(InputKeysCoroutine());
    }

    private IEnumerator InputKeysCoroutine()
    {
        GameObject panel = PopupManager.Instance.InstantPopup(dungeonEnd);
        var infoPanel = panel.GetComponent<InfoPanel>();
        infoPanel.SetInfoText($"던전 시간이 끝났습니다.");
        infoPanel.SetInsideInfoText($"아무 키를 눌러 던전에서 탈출");

        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        yield return new WaitForEndOfFrame();
        
        PopupManager.Instance.RemovePopup(panel.name);
        LoadingComponent.LoadScene("GameScene");
        LevelManager.Instance.LoadSaveRound();
    }
}