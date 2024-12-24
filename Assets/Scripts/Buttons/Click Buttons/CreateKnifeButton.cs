using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateKnifeButton : MonoBehaviour
{
    protected readonly string DoNotFunctionAlramText = $"현재 기능은 수행할 수 없습니다.";
    protected readonly string maxCountAlramText = $"최대치를 넘길 수 없습니다.";
    protected readonly string DungeonSceneName = $"DungeonScene";
    protected readonly string DoNotFunctionPanel = $"Warning Panel";

    private List<GameObject> uiKnifeObjs;
    private List<int> unlockedIDs = new List<int>();

    private ObjectPoolManager poolManager;
    private FunctionBarComponent functionBar;
    private KnifeCollectionBar knifeCollectBar;
    private Transform createPos;
    private Button myButton;
    private TextMeshProUGUI createdText;

    public static event Action OnCreateButton;

    private void Awake()
    {
        InitBehaviour();
    }

    private void OnEnable()
    {
        KnifeUIActivator.OnMerge += UnlockNextID;
        KnifeUIActivator.OnMerge += UpdateCreatedText;
    }

    private void OnDisable()
    {
        KnifeUIActivator.OnMerge -= UnlockNextID;
        KnifeUIActivator.OnMerge -= UpdateCreatedText;
    }

    private void InitBehaviour()
    {
        createdText = GetComponentInChildren<TextMeshProUGUI>();
        poolManager = ObjectPoolManager.Instance;

        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(CreateButton);
    }

    private void UpdateCreatedText()
    {
        createdText.text = $"칼 제작\n({knifeCollectBar.GetCreatedCurrentCount()} / {knifeCollectBar.GetCreatedMaxCount()})";
    }

    public void InitKnifeData(PlayerManager playerManager)
    {
        var player = playerManager.GetPlayer();
        var knifeData = player.GetComponent<KnifeData>();
        uiKnifeObjs = knifeData.GetUIKnifes();

        var uiObj = UIManager.Instance.gameObject;
        functionBar = uiObj.GetComponentInChildren<FunctionBarComponent>();
        knifeCollectBar = functionBar.GetKnifeCollectBar();

        createPos = knifeCollectBar.transform.GetChild(1).GetChild(0).GetChild(0);
    }

    public void InitPools()
    {
        foreach (var knife in uiKnifeObjs)
        {
            poolManager.InitObjectPool(knife);
        }

        unlockedIDs.Add(1);
        UpdateCreatedText();
    }

    public void CreateButton()
    {
        if (SceneStateManager.Instance.CurrentScene == DungeonSceneName)
        {
            var panel = PopupManager.Instance.InstantPopup(DoNotFunctionPanel);
            var warningPanel = panel.GetComponent<WarningPanel>();
            warningPanel.SetAlramPanelText(DoNotFunctionAlramText);
            return;
        }

        CreateRandomKnifes();
        OnCreateButton?.Invoke();
    }
    
    private GameObject CreateRandomKnifes()
    {
        if (knifeCollectBar.GetCreatedCurrentCount() >= knifeCollectBar.GetCreatedMaxCount())
        {
            var panel = PopupManager.Instance.InstantPopup(DoNotFunctionPanel);
            var warningPanel = panel.GetComponent<WarningPanel>();
            warningPanel.SetAlramPanelText(maxCountAlramText);

            return null;
        }

        if (unlockedIDs.Count == 0)
        {
            Debug.LogError($"생성 가능한 단계가 없습니다.");
            return null;
        }

        int randomID = unlockedIDs[UnityEngine.Random.Range(0, unlockedIDs.Count)];
        GameObject selectedKnife = uiKnifeObjs.Find(k => k.GetComponent<KnifeNextData>().NextID == randomID);

        if (selectedKnife == null)
        {
            Debug.LogError($"ID {randomID}에 해당하는 나이프 데이터가 없습니다.");
            return null;
        }

        RectTransform contentRect = createPos.GetComponent<RectTransform>();
        var rect = contentRect.rect;

        var knifeRectTrans = selectedKnife.GetComponent<RectTransform>();
        Vector2 halfSize = knifeRectTrans.rect.size * 0.5f;        

        float randomX = UnityEngine.Random.Range(-contentRect.rect.width / 2, contentRect.rect.width / 2);
        float randomY = UnityEngine.Random.Range(-contentRect.rect.height / 2, contentRect.rect.height / 2);

        randomX = Mathf.Clamp(randomX, rect.xMin + halfSize.x, rect.xMax - halfSize.x);
        randomY = Mathf.Clamp(randomY, rect.yMin + halfSize.y, rect.yMax - halfSize.y);

        Vector3 randomPos = new(randomX, randomY, selectedKnife.transform.position.z);

        GameObject finalKnife = poolManager.GetToPool(selectedKnife, createPos);
        finalKnife.transform.localScale = Vector3.one;

        if (finalKnife == null)
        {
            Debug.LogError("Failed to get knife from pool.");
            return null;
        }

        finalKnife.transform.localPosition = randomPos;

        knifeCollectBar.AddAttackKnifes(finalKnife);
        UpdateCreatedText();

        return finalKnife;
    }

    private void UnlockNextID()
    {
        foreach (var knife in knifeCollectBar.GetKnifesList())
        {
            var knifeData = knife.GetComponent<KnifeNextData>();
            if (knifeData != null && !unlockedIDs.Contains(knifeData.NextID))
            {
                unlockedIDs.Add(knifeData.NextID);
                Debug.Log($"Unlocked new knife ID: {knifeData.NextID}");
            }
        }
    }
}
