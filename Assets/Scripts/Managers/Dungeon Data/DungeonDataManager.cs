using SingletonBase.DontDestroySingleton;
using System.Collections;
using TMPro;
using UnityEngine;

public class DungeonDataManager : SingletonBase<DungeonDataManager>
{
    [SerializeField] private DungeonData currentDungeonData;
    private TextMeshProUGUI remaingTimerText;

    private float remaingTimer;

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
                    // TODO : Ä¸½¶¿ë ¸¸µé±â
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
        yield return new WaitForEndOfFrame();

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
            }

            int min = Mathf.FloorToInt(remaingTimer / 60f);
            int sec = Mathf.FloorToInt(remaingTimer % 60f);

            remaingTimerText.text = $"{min}:{sec:D2}";
        }
    }

    private void DungeonEnd()
    {
        StopAllCoroutines();
        remaingTimer = 0;
        UpdateTimerUI();

        var dungeonText = UIManager.Instance.GetDungeonText();
        dungeonText.gameObject.SetActive(false);

        StartCoroutine(InputKeysCoroutine());
    }

    private IEnumerator InputKeysCoroutine()
    {
        while (!Input.anyKeyDown)
        {
            UIManager.Instance.dungeonEndPanel.SetActive(true);
            yield return null;
        }

        yield return new WaitForEndOfFrame();

        UIManager.Instance.dungeonEndPanel.SetActive(false);
        LoadingComponent.LoadScene("GameScene");
        LevelManager.Instance.LoadSaveRound();
    }
}