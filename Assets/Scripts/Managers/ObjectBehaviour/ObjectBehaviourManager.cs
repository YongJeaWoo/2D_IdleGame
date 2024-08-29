using SingletonBase.DontDestroySingleton;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviourManager : SingletonBase<ObjectBehaviourManager>
{
    public event Action OnEnemyDeath;

    public void CallEnemyDeath()
    {
        OnEnemyDeath?.Invoke();
    }
}
