using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public class Bullet : MonoBehaviour, IPoolAble
{
    [SerializeField] private LayerMask _raycastMask;
    private bool _active;
    [Inject] private BulletManager _bulletManager;
    private readonly int _damage = 20;
    private Vector3 _previousPosition;
    private Vector3 _shootOrigin;
    private float _timeToReturnToPool;
    private Vector3 previousPosition;
    public Rigidbody Rigidbody { get; private set; }

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
        previousPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        var postion = transform.position - transform.forward * other.contactOffset;
        var result = GetCollisionInfo(previousPosition);

        var IHittable = other.GetComponent<IHittable>();
        IHittable?.Hit(_damage);

        if (result.Item1)
            _bulletManager.CreateImpact(SurfaceType.Wall, result.Item2, result.Item3);

        ReturnToPool();
    }

    public ObjectPool<GameObject> ParentPool { get; set; }

    public void Initilize(BulletManager bulletManager, float timeToReturnToPool, Vector3 origin)
    {
        _shootOrigin = origin;
        _bulletManager = bulletManager;
        _timeToReturnToPool = timeToReturnToPool;
        _active = true;
        Rigidbody = GetComponent<Rigidbody>();
        previousPosition = transform.position;
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