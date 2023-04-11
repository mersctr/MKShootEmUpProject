using UnityEngine;
using Zenject;

public class EnemyController : Enemy
{
    [SerializeField] private float _range;
    private float _distanceToThePlayer;
    [Inject] private PlayerController _playerController;
    [Inject] private Vitals _vitals;
    [Inject] private WeaponController _weaponController;

    private void Start()
    {
        _vitals.OnDeath += Vitals_OnDeath;
    }

    private void Update()
    {
        _distanceToThePlayer = Vector3.Distance(transform.position, _playerController.transform.position);
        _weaponController.TryToShoot(_distanceToThePlayer <= _range && _playerController.Vitals.IsAlive);
    }

    private void OnDestroy()
    {
        _vitals.OnDeath -= Vitals_OnDeath;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _range);
    }

    private void Vitals_OnDeath()
    {
        Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}