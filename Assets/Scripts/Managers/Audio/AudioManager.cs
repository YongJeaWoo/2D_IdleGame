using SingletonBase.DontDestroySingleton;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonBase<AudioManager>
{
    private readonly string AUDIO_PATH = $"/Ingame/Music";

    [Header("사용될 오디오 리스트")]
    [SerializeField] private List<AudioClip> clipList = new List<AudioClip>();

    private List<AudioSource> audioSources = new List<AudioSource>();
    private AudioSource loopAudioSource;

    public AudioSource CreateNewAudioSource()
    {
        GameObject newAudioName = new();
        ObjectPoolManager.Instance.InitObjectPool(newAudioName);
        var audioObj = ObjectPoolManager.Instance.GetToPool(newAudioName);
        AudioSource newSource = audioObj.AddComponent<AudioSource>();
        audioSources.Add(newSource);
        return newSource;
    }

    private AudioSource GetAvailableAudioSource()
    {
        foreach (var source in audioSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }

        }

         return CreateNewAudioSource();
    }

    public void Play(AudioClip _clip, bool _loop = false)
    {
        AudioSource source = _loop ? loopAudioSource : GetAvailableAudioSource();
        source.clip = _clip;
        source.loop = _loop;
        source.Play();
    }

    public AudioClip LoadClip(string _loadClipName)
    {
        AudioClip clip = clipList.Find(c =>c.name.Equals(_loadClipName));

        if (clip != null) return clip;

        clip = Resources.Load<AudioClip>($"{AUDIO_PATH}{_loadClipName}");

        if (clip != null) clipList.Add(clip);

        return clip;
    }
}
