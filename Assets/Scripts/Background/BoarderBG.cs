using UnityEngine;

public class BoarderBG : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject)
        {
            ReleaseObject(collision);
        }
    }

    private void ReleaseObject(Collider2D obj)
    {
        ObjectPoolManager.Instance.ReleaseToPool(obj.gameObject);
    }
}
