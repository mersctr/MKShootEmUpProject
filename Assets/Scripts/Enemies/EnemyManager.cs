using UnityEngine;
using Zenject;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject _suiciderPrefab;
    [SerializeField] private GameObject _moveableEnemyPrefab;
    [Inject] private PoolSystem _pool;

    public void SpawnSuiciders(int amount, SpawnPoint[] spawnPoints)
    {
        SpawnEnemy(amount, _suiciderPrefab, spawnPoints);
    }

    public void SpawnMovableEnemy(int amount, SpawnPoint[] spawnPoints)
    {
        SpawnEnemy(amount, _moveableEnemyPrefab, spawnPoints);
    }

    public void SpawnEnemy(int amount, GameObject enemyPrefab, SpawnPoint[] spawnPoints)
    {
        for (var i = 0; i < amount; i++)
        {
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            var enemy = _pool.GetItemFromPool(enemyPrefab);
            var spawnPosition = spawnPoint.transform.position;
            enemy.transform.position = spawnPosition;
        }
    }

    public void ReturnEnemy(IPoolable enemy)
    {
    }
}