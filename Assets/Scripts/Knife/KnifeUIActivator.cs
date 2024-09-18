using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class KnifeUIActivator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private GameObject nextPrefab;
    private KnifeNextData nextData;

    private Transform myTransform;
    private Vector2 originPosition;

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
        isClicked = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(myTransform.position, 0.5f);

        foreach (Collider2D hitCollider in colliders)
        {
            if (hitCollider != null && hitCollider.gameObject != gameObject && hitCollider.gameObject.name == gameObject.name)
            {
                MergeObjects(hitCollider.gameObject);
                return;
            }
        }
    }

    public void MergeObjects(GameObject otherObj)
    {
        if (nextPrefab == null) return;

        ObjectPoolManager.Instance.InitObjectPool(nextPrefab);

        GameObject mergedObj = ObjectPoolManager.Instance.GetToPool(nextPrefab, contentArea);

        if (mergedObj == null) return;

        knifeCollectionBar.AddAttackKnifes(mergedObj);
        knifeCollectionBar.RemoveAttackKnifes(otherObj);
        knifeCollectionBar.RemoveAttackKnifes(gameObject);

        var uiActivator = mergedObj.GetComponent<KnifeUIActivator>();
        if (uiActivator == null)
        {
            mergedObj.AddComponent<KnifeUIActivator>();
        }

        Transform mergedPos = mergedObj.GetComponent<Transform>();
        mergedPos.position = myTransform.position;

        ObjectPoolManager.Instance.ReleaseToPool(otherObj);
        ObjectPoolManager.Instance.ReleaseToPool(gameObject);

        OnMerge?.Invoke();
    }
}
