using UnityEngine;
using UnityEngine.Pool;

public class Muzzle : MonoBehaviour, IPoolAble
{
    [SerializeField] private float _timerToReturnToPool = 0.2f;
    private BulletManager _bulletManager;
    private float _currentTimer;
    private GameObject _prefab;

    private void Update()
    {
        if (_currentTimer <= 0)
            ParentPool.Release(gameObject);

        _currentTimer -= Time.deltaTime;
    }

    public ObjectPool<GameObject> ParentPool { get; set; }

    public void Init(BulletManager bulletManager)
    {
        _bulletManager = bulletManager;
        _currentTimer = _timerToReturnToPool;
    }
}