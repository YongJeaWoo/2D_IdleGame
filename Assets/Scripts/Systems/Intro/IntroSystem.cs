using UnityEngine;

public class IntroSystem : MonoBehaviour
{
    [SerializeField] private AudioClip titleClip;

    private void Start()
    {
        AudioManager.Instance.PlayBGM(titleClip);
    }
}
