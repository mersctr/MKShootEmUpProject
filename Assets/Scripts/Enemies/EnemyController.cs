using UnityEngine;
using Zenject;

public class EnemyController : Enemy
{
    [SerializeField] private float _range;
    [Inject] private PlayerController _playerController;
    [Inject] private Vitals _vitals;
    [Inject] private WeaponController _weaponController;
    private float _distanceToThePlayer;

    private void Start()
    {
        _vitals.OnDeath += Vitals_OnDeath;
    }
    
    private void OnDestroy()
    {
        _vitals.OnDeath -= Vitals_OnDeath;
    }
    
    private void Update()
    {
        _distanceToThePlayer = Vector3.Distance(transform.position, _playerController.transform.position);
        _weaponController.TryToShoot(_distanceToThePlayer <= _range && _playerController.Vitals.IsAlive);
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