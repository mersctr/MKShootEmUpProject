using UnityEngine;
using Zenject;

public class EffectManager : MonoBehaviour
{
    [Inject] private PoolSystem _poolSystem;

    public void CreateExplosion(GameObject prefab, Vector3 position)
    {
        var explosion = _poolSystem.GetItemFromPool(prefab);
        explosion.transform.position = position;
    }
}