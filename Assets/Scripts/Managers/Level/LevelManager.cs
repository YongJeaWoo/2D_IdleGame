using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using SingletonBase.DontDestroySingleton;

public class LevelManager : SingletonBase<LevelManager>
{
    [Header("현재 라운드")]
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

    // 라운드 변경 시 호출할 함수
    public void CallChangeRound()
    {
        currentRound++;
        OnRoundChange?.Invoke();
    }

    public int GetCurrentRound() => currentRound;
}
