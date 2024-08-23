using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using SingletonBase.DontDestroySingleton;

public class LevelManager : SingletonBase<LevelManager>
{
    [Header("���� ����")]
    [SerializeField] private int currentRound;

    public event Action OnRoundChange;

    private void Start()
    {
        StartCoroutine(TestCoroutine());
    }

    private IEnumerator TestCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);

            CallChangeRound();
        }
    }

    // ���� ���� �� ȣ���� �Լ�
    public void CallChangeRound()
    {
        currentRound++;
        OnRoundChange?.Invoke();
    }

    public int GetCurrentRound() => currentRound;
}
