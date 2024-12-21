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
        while (true)
        {
            var currentRound = LevelManager.Instance.GetCurrentRound();
            var ceilValue = Mathf.Ceil(currentRound * 1.4f);
            var maxCount = (int)Mathf.Max(2, ceilValue);
            int createdCount = Random.Range(1, maxCount + 1);

            spawnCount = createdCount;

            GameObject selectedPrefab;

            if (currentRound % 100 == 0)
            {
                selectedPrefab = enemiesPrefab.Length >= 10 ? enemiesPrefab[9] :
                    enemiesPrefab.Length >= 5 ? enemiesPrefab[4] : enemiesPrefab[0];
            }
            else if (currentRound % 10 == 0)
            {
                selectedPrefab = enemiesPrefab.Length >= 5 ? enemiesPrefab[4] : enemiesPrefab[0];
            }
            else
            {
                selectedPrefab = enemiesPrefab[0];
            }

            for (int i = 0; i < createdCount; i++)
            {
                var enemyObj = ObjectPoolManager.Instance.GetToPool(selectedPrefab, spawnPos);

                if (enemyObj != null)
                {
                    if (enemyObj.TryGetComponent<EnemyHealth>(out var health))
                    {
                        health.OnDeath += EnemyDeath;
                        health.SetCurrentHp(CalculateHealth(currentRound));
                    }
                }

                yield return new WaitForSeconds(5f);
            }

            yield return new WaitUntil(() => killCount >= spawnCount);

            NextRound();
        }
    }

    private int CalculateHealth(int round)
    {
        return Mathf.CeilToInt(round * 10 * 1.2f);
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
