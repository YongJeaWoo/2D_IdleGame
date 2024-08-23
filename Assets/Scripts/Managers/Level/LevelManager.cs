using SingletonBase.DontDestroySingleton;
using System;
using UnityEngine;

public class LevelManager : SingletonBase<LevelManager>
{
    [Header("현재 라운드")]
    [SerializeField] private int currentRound;

    public event Action OnRoundChange;
    public event Action OnChangeHealth;

    // 라운드 변경 시 호출할 함수
    public void CallChangeRound()
    {
        currentRound++;
        OnRoundChange?.Invoke();
    }

    public void CallChangeHealth()
    {
        OnChangeHealth?.Invoke();
    }

    public int GetCurrentRound() => currentRound;
}
