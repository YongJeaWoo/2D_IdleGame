using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class KnifeUIActivator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private GameObject nextPrefab;
    private KnifeNextData nextData;

    private Transform myTransform;
    private Vector2 originPosition;

    private bool isDragging;
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
        myTransform = GetComponent<Transform>();
        contentArea = transform.parent.GetComponent<RectTransform>();

        knifeCollectionBar = UIManager.Instance.gameObject.GetComponentInChildren<KnifeCollectionBar>();
        nextData = GetComponent<KnifeNextData>();

        nextPrefab = nextData.GetNextPrefab();
        if (nextPrefab == null) return;
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDragging = true;
        if (!isClicked) return;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        worldPosition.z = myTransform.position.z; 

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(contentArea, eventData.position, Camera.main, out localPoint);

        Rect rect = contentArea.rect;
        localPoint.x = Mathf.Clamp(localPoint.x, rect.xMin, rect.xMax);
        localPoint.y = Mathf.Clamp(localPoint.y, rect.yMin, rect.yMax);

        Vector3 clampedPosition = contentArea.TransformPoint(localPoint);
        myTransform.position = clampedPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        originPosition = myTransform.position;

        isClicked = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        isClicked = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(myTransform.position, 0.5f);

        Debug.Log($"마우스를 놓았는가?");

        foreach (Collider2D hitCollider in colliders)
        {
            if (hitCollider != null && hitCollider.gameObject != gameObject && hitCollider.gameObject.name == gameObject.name)
            {
                Debug.Log($"물체가 닿았나?");
                MergeObjects(hitCollider.gameObject);
                Debug.Log($"함수 실행 되었는가?");
                return;
            }
        }
    }

    private void MergeObjects(GameObject otherObj)
    {
        if (nextPrefab == null)
        {
            Debug.LogWarning($"null 인가?");
            return;
        }

        Debug.Log($"MergeObjects 함수에 들어왔다");

        if (!ObjectPoolManager.Instance.IsPoolInitialized(nextPrefab))
        {
            ObjectPoolManager.Instance.InitObjectPool(nextPrefab);
        }

        GameObject mergedObj = ObjectPoolManager.Instance.GetToPool(nextPrefab, contentArea);

        if (mergedObj == null) return;

        knifeCollectionBar.AddAttackKnifes(mergedObj);
        knifeCollectionBar.RemoveAttackKnifes(otherObj);
        knifeCollectionBar.RemoveAttackKnifes(gameObject);

        knifeCollectionBar.RemoveCreatedCount();

        Transform mergedPos = mergedObj.GetComponent<Transform>();
        mergedPos.position = myTransform.position;

        ObjectPoolManager.Instance.ReleaseToPool(otherObj);
        ObjectPoolManager.Instance.ReleaseToPool(gameObject);

        OnMerge?.Invoke();
    }
}
