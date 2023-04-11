using UnityEngine;
using Zenject;

public class CollectableGIver : MonoBehaviour
{
    [SerializeField] private float _yPosition = 1.5f;
    [SerializeField] private LifePack _lifePrefab;
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
        var position = transform.position;
        position.y = _yPosition;

        Instantiate(_lifePrefab, position, Quaternion.identity);
        Destroy(gameObject);
    }
}