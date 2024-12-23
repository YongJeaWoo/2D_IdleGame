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

    // 라운드별로 몬스터 소환
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
            bool isBossRound = currentRound % 10 == 0;

            GameObject selectedPrefab = SelectEnemyPrefab(currentRound); // 라운드에 맞는 적 선택

            int createdCount = isBossRound ? 1 : Random.Range(1, Mathf.RoundToInt(currentRound * 1.4f) + 1);  // 몬스터 생성 수 계산
            spawnCount = createdCount;

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

                float randomTime = Random.Range(3f, 5f);
                yield return new WaitForSeconds(randomTime);  // 생성 간격 조절
            }

            yield return new WaitUntil(() => killCount >= spawnCount);  // 모든 적이 죽을 때까지 대기

            NextRound();
        }
    }

    // 적의 체력 계산
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

    // 적 프리팹을 라운드에 맞게 선택
    private GameObject SelectEnemyPrefab(int currentRound)
    {
        if (currentRound % 10 == 0)
        {
            return SelectBossEnemyPrefab(currentRound); // 10, 20, 30, ... 라운드에서 보스 몬스터 처리
        }
        else
        {
            return SelectNormalEnemyPrefab(currentRound);
        }
    }

    // 10의 배수 라운드에서는 5번째 몬스터 (인덱스 4)
    private GameObject SelectBossEnemyPrefab(int currentRound)
    {
        // 10단위 라운드 (10, 20, 30...)에서는 enemiesPrefab의 5번째 몬스터 선택
        if (currentRound % 100 != 0) // 100단위가 아닌 경우
        {
            return enemiesPrefab[4]; // 5번째 몬스터
        }
        else
        {
            // 100단위부터는 10번째 몬스터 (110, 120, 130... 라운드)
            return SelectHighRoundBoss(currentRound);
        }
    }

    // 100단위 이상에서는 10번째, 15번째, 20번째 몬스터 소환
    private GameObject SelectHighRoundBoss(int currentRound)
    {
        int multiplier = Mathf.FloorToInt(currentRound / 100f); // 100단위 계산
        int bossIndex = 9 + (multiplier - 1) * 5; // 5의 배수 인덱스 계산

        // 1000 단위 이후부터 적용
        if (multiplier >= 10)
        {
            bossIndex = 9 + (multiplier - 1) * 5;
        }

        // 배열 크기를 초과할 경우 반복적으로 5의 배수 인덱스 생성
        if (bossIndex >= enemiesPrefab.Length)
        {
            bossIndex = (bossIndex % enemiesPrefab.Length) - (bossIndex % 5); // 가장 가까운 5의 배수
            if (bossIndex < 0)
            {
                bossIndex = Mathf.Max(0, enemiesPrefab.Length - 1); // 최소 값 보정
            }
        }

        return enemiesPrefab[bossIndex];
    }

    // 일반 몬스터 처리
    private GameObject SelectNormalEnemyPrefab(int currentRound)
    {
        int maxIndex;

        // 1. 10의 단위 이하 라운드 (1~99)
        if (currentRound < 100)
        {
            int onesPlace = currentRound % 10;

            if (onesPlace <= 2)
            {
                maxIndex = Mathf.Min(0, enemiesPrefab.Length - 1); // 1번째 몬스터만
            }
            else if (onesPlace <= 4)
            {
                maxIndex = Mathf.Min(1, enemiesPrefab.Length - 1); // 1, 2번째 몬스터
            }
            else if (onesPlace <= 6)
            {
                maxIndex = Mathf.Min(2, enemiesPrefab.Length - 1); // 1, 2, 3번째 몬스터
            }
            else
            {
                maxIndex = Mathf.Min(3, enemiesPrefab.Length - 1); // 1, 2, 3, 4번째 몬스터
            }
        }
        // 2. 100 이상의 라운드
        else
        {
            int hundredsPlace = Mathf.FloorToInt(currentRound / 100f); // 100단위 계산
            maxIndex = Mathf.Min(4 + hundredsPlace, enemiesPrefab.Length - 1); // 크기 제한
        }

        // 일반 몬스터 선택
        int randomIndex = Random.Range(0, maxIndex + 1);
        return enemiesPrefab[randomIndex];
    }

    // 모든 적을 오브젝트 풀로 반환하고 스폰을 멈추는 메서드
    public void StopSpawningAndClearEnemies()
    {
        isSpawning = false;
        killCount = 0;
        spawnCount = 0;

        var enemies = FindObjectsOfType<EnemyHealth>();
        foreach (var enemy in enemies)
        {
            ObjectPoolManager.Instance.ReleaseToPool(enemy.gameObject);
        }
    }
}
