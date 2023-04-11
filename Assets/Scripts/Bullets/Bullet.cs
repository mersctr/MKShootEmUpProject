using System;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public class Bullet : MonoBehaviour, IPoolAble
{
    [SerializeField] private LayerMask _raycastMask;
    [Inject] private BulletManager _bulletManager;
    private bool _active;
    private readonly int _damage = 20;
    private Vector3 _previousPosition;
    private Vector3 _shootOrigin;
    private float _timeToReturnToPool;
    public Rigidbody Rigidbody { get; private set; }
    public ObjectPool<GameObject> ParentPool { get; set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!_active)
            return;

        if (_timeToReturnToPool <= 0)
            ReturnToPool();

        _timeToReturnToPool -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        _previousPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        var result = GetCollisionInfo(_previousPosition);

        var iHittable = other.GetComponent<IHittable>();
        iHittable?.Hit(_damage);

        if (result.Item1)
            _bulletManager.CreateImpact(SurfaceType.Wall, result.Item2, result.Item3);

        ReturnToPool();
    }

    public void Initialize(BulletManager bulletManager, float timeToReturnToPool, Vector3 origin)
    {
        _shootOrigin = origin;
        _bulletManager = bulletManager;
        _timeToReturnToPool = timeToReturnToPool;
        _active = true;
        _previousPosition = transform.position;
     
    }

    public void ReturnToPool()
    {
        _active = false;
        ParentPool.Release(gameObject);
    }
    
    private (bool, Vector3, Quaternion) GetCollisionInfo(Vector3 prevPos)
    {
        RaycastHit hit;
        var direction = transform.position - prevPos;
        var ray = new Ray(prevPos, direction);
        var dist = Vector3.Distance(transform.position, prevPos);

        if (Physics.Raycast(ray, out hit, dist, _raycastMask))
        {
            transform.position = hit.point;
            var rot = Quaternion.FromToRotation(Vector3.forward, hit.normal);
            var pos = hit.point;

            return (true, pos, rot);
        }

        return default;
    }
}