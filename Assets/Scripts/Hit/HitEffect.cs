using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public void EndHitEffect()
    {
        ObjectPoolManager.Instance.ReleaseToPool(gameObject);
    }
}
