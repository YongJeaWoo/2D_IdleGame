using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class KnifeUIActivator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField]
    private float mergeDistanceThreshold = 0.2f;

    private KnifeNextData nextData;
    private RectTransform myTransform;
    private bool isClicked = false;
    private RectTransform contentArea;
    private KnifeCollectionBar knifeCollectionBar;

    public static event Action OnMerge;

    private void Start()
    {
        InitActivator();
    }

    private void InitActivator()
    {
        myTransform = GetComponent<RectTransform>();
        knifeCollectionBar = UIManager.Instance.gameObject.GetComponentInChildren<KnifeCollectionBar>();
        contentArea = knifeCollectionBar.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<RectTransform>();
        nextData = GetComponent<KnifeNextData>();
        isClicked = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isClicked || myTransform == null || contentArea == null) return;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(contentArea, eventData.position, Camera.main, out Vector2 localPoint))
        {
            return;
        }

        Vector2 halfSize = myTransform.rect.size * 0.5f;
        Rect rect = contentArea.rect;
        localPoint.x = Mathf.Clamp(localPoint.x, rect.xMin + halfSize.x, rect.xMax - halfSize.x);
        localPoint.y = Mathf.Clamp(localPoint.y, rect.yMin + halfSize.y, rect.yMax - halfSize.y);

        myTransform.localPosition = localPoint;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isClicked = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isClicked = false;

        RectTransform myRect = myTransform.GetComponent<RectTransform>();

        foreach (var knifeObj in knifeCollectionBar.GetKnifesList())
        {
            if (knifeObj == gameObject) continue;

            var otherData = knifeObj.GetComponent<KnifeNextData>();

            if (otherData != null && otherData.NextID == nextData.NextID)
            {
                RectTransform otherRect = knifeObj.GetComponent<RectTransform>();

                float distance = Vector3.Distance(myRect.position, otherRect.position);

                if (distance < mergeDistanceThreshold)
                {
                    MergeObjects(knifeObj);
                    return;
                }
            }
        }
    }

    public void MergeObjects(GameObject otherObj)
    {
        var otherData = otherObj.GetComponent<KnifeNextData>();
        if (otherData == null)
        {
            Debug.LogError("다른 오브젝트에 KnifeNextData가 없습니다.");
            return;
        }

        if (nextData.NextID != otherData.NextID)
        {
            Debug.LogError("다른 오브젝트와 NextID가 다릅니다.");
            return;
        }

        int newNextID = nextData.GetNextID(nextData.NextID);

        var player = PlayerManager.Instance.GetPlayer();
        var knifeData = player.GetComponent<KnifeData>();
        var knifeList = knifeData.GetUIKnifes();

        var nextPrefab = knifeList.FirstOrDefault(k => k.GetComponent<KnifeNextData>().NextID == newNextID);
        if (nextPrefab == null)
        {
            Debug.LogWarning("현재 최대 레벨치 입니다. 더 이상 병합할 오브젝트가 없습니다.");
            return;
        }

        ObjectPoolManager.Instance.InitObjectPool(nextPrefab);
        GameObject mergedObj = ObjectPoolManager.Instance.GetToPool(nextPrefab, contentArea);

        if (mergedObj == null)
        {
            Debug.LogError("병합된 오브젝트가 풀에서 가져와지지 않았습니다.");
            return;
        }

        if (mergedObj.TryGetComponent<KnifeNextData>(out var mergedData))
        {
            mergedData.NextID = newNextID;
        }
        else
        {
            Debug.LogError("병합된 오브젝트에 KnifeNextData가 없습니다.");
            return;
        }

        mergedObj.transform.localScale = Vector3.one;
        RectTransform mergedPos = mergedObj.GetComponent<RectTransform>();
        mergedPos.position = myTransform.position;

        knifeCollectionBar.AddAttackKnifes(mergedObj);
        knifeCollectionBar.RemoveAttackKnifes(otherObj);
        knifeCollectionBar.RemoveAttackKnifes(gameObject);

        ObjectPoolManager.Instance.ReleaseToPool(otherObj);
        ObjectPoolManager.Instance.ReleaseToPool(gameObject);

        OnMerge?.Invoke();
    }
}
