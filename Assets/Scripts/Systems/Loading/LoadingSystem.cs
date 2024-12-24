using System.Collections;
using UnityEngine;

public class LoadingSystem : MonoBehaviour
{
    private readonly float duration = 2f;

    private void Start()
    {
        FadeoutTheBGMVolume();
    }

    private void FadeoutTheBGMVolume()
    {
        var volume = AudioManager.Instance.GetBGMSource();

        if (volume != null)
        {
            StartCoroutine(FadeoutVolumeCoroutine(volume, duration));
        }
    }

    private IEnumerator FadeoutVolumeCoroutine(AudioSource source, float duration)
    {
        float startVolume = source.volume;

        while (source.volume > 0.2f)
        {
            source.volume -= startVolume * Time.deltaTime / duration;

            if (source.volume <= 0.2f)
            {
                source.volume = 0.2f;
                break;
            }

            yield return null;
        }

        AudioManager.Instance.StopBGM();
    }
}
