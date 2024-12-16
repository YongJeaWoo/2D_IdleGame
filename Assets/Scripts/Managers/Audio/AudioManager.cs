using SingletonBase.DontDestroySingleton;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonBase<AudioManager>
{
    private List<AudioSource> sfxSources = new List<AudioSource>();
    private AudioSource bgmSource;

    public void PlayBGM(AudioClip clip, bool loop = true, float volume = 1.0f)
    {
        if (bgmSource == null)
        {
            GameObject bgmObject = new GameObject("BGM_AudioSource");
            bgmObject.transform.SetParent(transform);
            bgmSource = bgmObject.AddComponent<AudioSource>();
            bgmSource.playOnAwake = false;
        }

        if (clip == null)
        {
            Debug.LogError("BGM Clip is null");
            return;
        }

        if (bgmSource.clip == clip && bgmSource.isPlaying)
        {
            return; 
        }

        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.volume = volume;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        if (bgmSource != null)
        {
            bgmSource.Stop();
            bgmSource.clip = null;
        }
    }

    public void PlaySFX(AudioClip clip, bool loop = false, float volume = 1.0f)
    {
        if (clip == null)
        {
            Debug.LogError("SFX Clip is null");
            return;
        }

        AudioSource source = GetAvailableSFXSource();
        source.clip = clip;
        source.volume = volume;
        source.loop = loop;
        source.Play();
    }

    public void StopSFX(AudioClip clip)
    {
        if (sfxSources != null)
        {
            var source = sfxSources.Find(x => x.clip == clip);
            source.Stop();
        }
    }

    private AudioSource CreateNewSFXSource()
    {
        GameObject sfxObject = new GameObject("SFX_AudioSource");
        sfxObject.transform.SetParent(transform);
        AudioSource newSource = sfxObject.AddComponent<AudioSource>();
        newSource.playOnAwake = false;
        sfxSources.Add(newSource);
        return newSource;
    }

    private AudioSource GetAvailableSFXSource()
    {
        foreach (var source in sfxSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }

        return CreateNewSFXSource();
    }

    public AudioSource GetBGMSource() => bgmSource;
}
