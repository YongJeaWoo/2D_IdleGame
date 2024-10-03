using SingletonBase.DontDestroySingleton;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonBase<AudioManager>
{
    private readonly string AUDIO_PATH = $"/Ingame/Music";

    [Header("사용될 오디오 리스트")]
    [SerializeField] private List<AudioClip> clipList = new List<AudioClip>();

    public AudioSource audioSource;

    #region Utils
    public void Play()
    {
        audioSource.Play();
    }

    public void Play(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public void Pause()
    {
        audioSource.Pause();
    }

    public void UnPause()
    {
        audioSource.UnPause();
    }
    #endregion

    public AudioClip LoadClip(string _loadClipName)
    {
        AudioClip clip = clipList.Find(c =>c.name.Equals(_loadClipName));

        if (clip != null) return clip;

        clip = Resources.Load<AudioClip>($"{AUDIO_PATH}{_loadClipName}");

        if (clip != null) clipList.Add(clip);

        return clip;
    }
}
