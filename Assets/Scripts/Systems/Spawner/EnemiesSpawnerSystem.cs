using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawnerSystem : MonoBehaviour
{
    [Header("�� ������")]
    [SerializeField] private GameObject[] enemiesPrefab;
    [Header("�� ���� ��ġ")]
    [SerializeField] private Transform spawnPos;

    private ObjectPool pool;

    private void OnEnable()
    {
        ObjectPoolManager.Instance.InitObjectPool(enemiesPrefab[0], ref pool);
        LevelManager.Instance.OnRoundChange += SpawnEnemies;
    }

    private void OnDisable()
    {
        LevelManager.Instance.OnRoundChange -= SpawnEnemies;
    }    

    // TODO : ���庰 ���� �� ���� 
    public void SpawnEnemies()
    {
        // var currentRound = LevelManager.Instance.GetCurrentRound();

        var enemiesObjs = pool.GetPoolObject(pool.transform);
        enemiesObjs.transform.position = spawnPos.position;
    }
}
