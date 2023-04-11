using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private GameObject _muzzlePrefab;
    [SerializeField] private GameObject _shellPrefab;
    [SerializeField] private Transform _shellOriginPoint;
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private float _defaultFireRate = 1;
    [Inject] private BulletManager _bulletManager;
    private float _currentFireRate;
    private float _customFireRate;
    private bool _isshooting;
    private bool _useCustomFireRate;
    private bool _useShells;
    private float _fireRate => _useCustomFireRate ? _customFireRate : _defaultFireRate;


    private void FixedUpdate()
    {
        if (_isshooting)
        {
            if (_currentFireRate <= 0)
            {
                Shoot();
                _currentFireRate = _fireRate;
            }

            _currentFireRate -= Time.deltaTime;
        }
    }

    public event Action OnShoot;

    public void TryToShoot(bool isShoting)
    {
        _isshooting = isShoting;
    }

    private void Shoot()
    {
        _bulletManager.ShootABullet(_bulletPrefab, Random.Range(2800, 4100), _bulletPoint);
        _bulletManager.CreateMuzzle(_muzzlePrefab, _bulletPoint);

        if (_shellPrefab != null && _useShells)
            _bulletManager.CreateShells(_shellPrefab, _shellOriginPoint.position);

        OnShoot?.Invoke();
    }

    public void SetShellUsage(bool useShells)
    {
        _useShells = useShells;
    }

    public void SetFireRate(bool useCustomFireRate, float customFireRate)
    {
        _useCustomFireRate = useCustomFireRate;
        _customFireRate = customFireRate;
    }
}