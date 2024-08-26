using SingletonBase.DontDestroySingleton;
using System;
using UnityEngine;

public class LevelManager : SingletonBase<LevelManager>
{
    [Header("���� ����")]
    [SerializeField] private int currentRound;

    public event Action OnRoundChange;

    // ���� ���� �� ȣ���� �Լ�
    public void CallChangeRound()
    {
        currentRound++;
        OnRoundChange?.Invoke();
    }

    public int GetCurrentRound() => currentRound;
}
