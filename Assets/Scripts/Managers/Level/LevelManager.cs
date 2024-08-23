using SingletonBase.DontDestroySingleton;
using System;
using UnityEngine;

public class LevelManager : SingletonBase<LevelManager>
{
    [Header("���� ����")]
    [SerializeField] private int currentRound;

    public event Action OnRoundChange;
    public event Action OnChangeHealth;

    // ���� ���� �� ȣ���� �Լ�
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
