using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateKnifeButton : MonoBehaviour
{
    private List<GameObject> uiKnifeObjs;

    private ObjectPoolManager poolManager;
    private PlayerSystem playerSystem;
    private FunctionBarComponent functionBar;
    private KnifeCollectionBar knifeCollectBar;
    private Transform createPos;
    private Button myButton;
    private TextMeshProUGUI createdText;

    public static event Action OnCreateButton;

    private void Awake()
    {
        GetComponents();
    }

    private void Start()
    {
        InitKnifeData();
        InitPools();

        UpdateCreatedText();
    }

    private void OnEnable()
    {
        KnifeUIActivator.OnMerge += UpdateCreatedText;
    }

    private void OnDisable()
    {
        KnifeUIActivator.OnMerge -= UpdateCreatedText;
    }

    private void GetComponents()
    {
        playerSystem = FindObjectOfType<PlayerSystem>();
        createdText = GetComponentInChildren<TextMeshProUGUI>();
        poolManager = ObjectPoolManager.Instance;
    }

    private void UpdateCreatedText()
    {
        createdText.text = $"칼 제작\n({knifeCollectBar.GetCreatedCurrentCount()} / {knifeCollectBar.GetCreatedMaxCount()})";
    }

    private void InitKnifeData()
    {
        var player = playerSystem.GetPlayer();
        var knifeData = player.GetComponent<KnifeData>();
        uiKnifeObjs = knifeData.GetUIKnifes();

        var uiObj = UIManager.Instance.gameObject;
        functionBar = uiObj.GetComponentInChildren<FunctionBarComponent>();
        knifeCollectBar = functionBar.GetKnifeCollectBar();

        createPos = knifeCollectBar.transform.GetChild(1).GetChild(0).GetChild(0);

        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(CreateButton);
    }

    private void InitPools()
    {
        foreach (var knife in uiKnifeObjs)
        {
            poolManager.InitObjectPool(knife);
        }
    }

    public void CreateButton()
    {
        CreateRandomKnifes();
        OnCreateButton?.Invoke();
    }

    private GameObject CreateRandomKnifes()
    {
        if (knifeCollectBar.GetCreatedCurrentCount() >= knifeCollectBar.GetCreatedMaxCount())
        {
            return null;
        }

        if (uiKnifeObjs.Count == 0)
        {
            Debug.LogError($"현재 가진 칼의 데이터가 존재하지 않음");
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, uiKnifeObjs.Count);
        GameObject selectedKnife = uiKnifeObjs[randomIndex];

        RectTransform contentRect = createPos.GetComponent<RectTransform>();

        float randomX = UnityEngine.Random.Range(-contentRect.rect.width / 2, contentRect.rect.width / 2);
        float randomY = UnityEngine.Random.Range(-contentRect.rect.height / 2, contentRect.rect.height / 2);

        Vector3 randomPos = new(randomX, randomY, selectedKnife.transform.position.z);

        GameObject finalKnife = poolManager.GetToPool(selectedKnife, createPos);
        finalKnife.transform.localScale = Vector3.one;

        if (finalKnife == null)
        {
            Debug.LogError("Failed to get knife from pool.");
            return null;
        }

        finalKnife.transform.localPosition = randomPos;

        if (knifeCollectBar == null)
        {
            Debug.LogError("KnifeCollectionBar 컴포넌트가 존재하지 않습니다.");
            return null;
        }

        knifeCollectBar.AddAttackKnifes(finalKnife);

        UpdateCreatedText();

        return finalKnife;
    }
}
