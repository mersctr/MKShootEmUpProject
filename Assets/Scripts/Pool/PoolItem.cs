using UnityEngine;
using UnityEngine.Pool;

public class PoolItem : MonoBehaviour, IPoolAble
{
    [SerializeField] private float _timerToPoolReales;
    private float _currentTimer;
    private GeneralPool _parentPool;

    public int PrefabPoolItemId { get; set; }

    private void Update()
    {
        if (_currentTimer <= 0)
            ReleaseToPool();

        _currentTimer -= Time.deltaTime;
    }

    private void OnEnable()
    {
        _currentTimer = _timerToPoolReales;
    }

    public ObjectPool<GameObject> ParentPool { get; set; }

    private void ReleaseToPool()
    {
        ParentPool.Release(gameObject);
    }
}