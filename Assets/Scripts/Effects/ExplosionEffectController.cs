using UnityEngine;
using Zenject;

public class ExplosionEffectController : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;
    [Inject] private EffectManager _effectManager;
    [Inject] private Vitals _vitals;

    private void Awake()
    {
        _vitals.OnDeath += OnDeath;
    }

    private void OnDestroy()
    {
        _vitals.OnDeath -= OnDeath;
    }

    private void OnDeath()
    {
        _effectManager.CreateExplosion(_explosionPrefab, transform.position);
    }
}