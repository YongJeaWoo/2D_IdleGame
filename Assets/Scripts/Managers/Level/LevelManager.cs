using SingletonBase.DontDestroySingleton;
using System;
using UnityEngine;

public class LevelManager : SingletonBase<LevelManager>
{
    [Header("현재 라운드")]
    [SerializeField] private int currentRound;

    [Header("라운드 데이터")]
    [SerializeField] private RoundData roundData;

    public event Action OnRoundChange;

    private void Start()
    {
        InitLevelData();
    }

    private void InitLevelData()
    {
        if (roundData != null)
        {
            currentRound = roundData.LoadRound();
        }
        else
        {
            currentRound = 1;
        }
    }

    // 라운드 변경 시 호출할 함수
    public void CallChangeRound()
    {
        currentRound++;
        SaveCurrentRound();
        OnRoundChange?.Invoke();
    }

    public void SaveCurrentRound()
    {
        if (roundData != null)
        {
            roundData.SaveRound(currentRound);
        }
        else
        {
            Debug.LogWarning($"라운드 데이터가 없습니다.");
        }
    }

    public void LoadSaveRound()
    {
        if (roundData != null)
        {
            currentRound = roundData.LoadRound();
            OnRoundChange?.Invoke();
        }
        else
        {
            Debug.LogWarning($"라운드 데이터가 없습니다.");
        }
    }

    public int GetCurrentRound() => currentRound;
}
