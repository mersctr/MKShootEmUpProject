using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public class EnemySuicider : Enemy, IPoolAble
{
    [SerializeField] private int _damage;
    [SerializeField] private float _minDistanceToPlayer;
    [Inject] private PlayerController _player;
    [Inject] private Vitals _vitals;
    private IHittable _playerHittable;

    private void Start()
    {
        _vitals.OnDeath += OnDeath;
    }

    private void Update()
    {
        if (!_player.Vitals.IsAlive)
            return;

        if (isPlayerClose())
            _vitals.Die();
    }

    private void OnDestroy()
    {
        _vitals.OnDeath -= OnDeath;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _minDistanceToPlayer);
    }

    public ObjectPool<GameObject> ParentPool { get; set; }

    private void OnDeath()
    {
        if (isPlayerClose())
            DealDamageToPlayer();

        ParentPool.Release(gameObject);
    }

    private bool isPlayerClose()
    {
        return Vector3.Distance(transform.position, _player.transform.position) <= _minDistanceToPlayer;
    }

    private void DealDamageToPlayer()
    {
        var hittable = _player.GetComponent<IHittable>();
        hittable.Hit(_damage);
    }
}