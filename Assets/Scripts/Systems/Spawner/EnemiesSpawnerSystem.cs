using System.Collections;
using UnityEngine;

public class EnemiesSpawnerSystem : MonoBehaviour
{
    [Header("적 프리팹")]
    [SerializeField] private GameObject[] enemiesPrefab;
    [Header("적 생성 위치")]
    [SerializeField] private Transform spawnPos;

    private int killCount;
    private int spawnCount;
    private bool isSpawning = false;

    private void Start()
    {
        foreach (var enemyPrefab in enemiesPrefab)
        {
            ObjectPoolManager.Instance.InitObjectPool(enemyPrefab);
        }

        SpawnEnemies();
    }

    private void OnEnable()
    {
        LevelManager.Instance.OnRoundChange += SpawnEnemies;
    }

    private void OnDisable()
    {
        LevelManager.Instance.OnRoundChange -= SpawnEnemies;
    }    

    // TODO : 라운드별 생성 수 지정 
    public void SpawnEnemies()
    {
        if (isSpawning) return;
        isSpawning = true;

        killCount = 0;
        spawnCount = 0;

        StartCoroutine(SpawnEnemiesCoroutine());
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
        // TODO : 추후 나중에 변경이 필요함 (던전 진입 시 멈추는 기능)
        while (true)
        {
            var currentRound = LevelManager.Instance.GetCurrentRound();
            var ceilValue = Mathf.Ceil(currentRound * 1.4f);
            var maxCount = (int)Mathf.Max(2, ceilValue);
            int createdCount = Random.Range(1, maxCount + 1);

            spawnCount = createdCount;

            int enemyIndex = 0;

            for (int i = 0; i < createdCount; i++)
            {
                var enemyPrefab = enemiesPrefab[enemyIndex];
                var enemyObj = ObjectPoolManager.Instance.GetToPool(enemyPrefab, spawnPos);
                
                if (enemyObj != null)
                {
                    if (enemyObj.TryGetComponent<EnemyHealth>(out var health))
                    {
                        health.OnDeath += EnemyDeath;
                    }
                }

                enemyIndex = (enemyIndex + 1) % enemiesPrefab.Length;

                yield return new WaitForSeconds(5f);
            }

            yield return new WaitUntil(() => killCount >= spawnCount);

            NextRound();
        }
    }

    private void EnemyDeath()
    {
        killCount++;
    }

    private void NextRound()
    {
        LevelManager.Instance.CallChangeRound();
        killCount = 0;
        spawnCount = 0;
    }
}
