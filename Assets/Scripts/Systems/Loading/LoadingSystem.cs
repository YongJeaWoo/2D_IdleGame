using System.Collections;
using UnityEngine;

public class LoadingSystem : MonoBehaviour
{
    private readonly float duration = 2f;
    public void FadeoutTheBGMVolume()
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

        while (source.volume > 0)
        {
            source.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        source.volume = 0.2f;
        AudioManager.Instance.StopBGM();
    }
}
