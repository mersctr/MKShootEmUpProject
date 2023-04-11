using UnityEngine;
using Zenject;

public class LevelSection : MonoBehaviour
{
    [SerializeField] private int _suicidersToSpawn;

    [SerializeField] private LevelTrigger _levelTriegger;
    [SerializeField] private Collider _collider;
    private EnemyController[] _enemyControllers;
    [Inject] private EnemyManager _enemyManager;

    private bool _entered;

    private SpawnPoint[] _spawnPoints;

    private void Awake()
    {
        _spawnPoints = GetComponentsInChildren<SpawnPoint>(true);
        _enemyControllers = GetComponentsInChildren<EnemyController>(true);
        _levelTriegger = GetComponentInChildren<LevelTrigger>();

        _levelTriegger.OnLevelEnteredEvent += LevelTrigger_OnLevelTrigger;
        SetEnemies(false);
    }

    private void OnDestroy()
    {
        _levelTriegger.OnLevelEnteredEvent -= LevelTrigger_OnLevelTrigger;
    }

    private void LevelTrigger_OnLevelTrigger()
    {
        _enemyManager.SpawnSuiciders(_suicidersToSpawn, _spawnPoints);
        SetEnemies(true);
    }

    private void SetEnemies(bool status)
    {
        foreach (var enemy in _enemyControllers)
            enemy.gameObject.SetActive(status);
    }
/// <summary>
/// It will turn on or off colliders for level. Find better name so comment is not needed
/// </summary>
/// <param name="status"></param>
    public void EnableSection(bool status)
    {
        _collider.enabled = status;
    }
}