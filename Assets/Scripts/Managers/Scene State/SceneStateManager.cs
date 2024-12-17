using SingletonBase.DontDestroySingleton;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateManager : SingletonBase<SceneStateManager>
{
    public string CurrentScene { get; private set; }

    public Action<string> OnSceneLoadAction;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnCurrentSceneChangeMethod(string targetSceneName)
    {
        CurrentScene = targetSceneName;
        OnSceneLoadAction?.Invoke(CurrentScene);
        Debug.Log(CurrentScene);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CurrentScene = scene.name;
    }
}

