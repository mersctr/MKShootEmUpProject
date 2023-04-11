using System.Collections.Generic;
using UnityEngine;

public class PoolSystem : MonoBehaviour
{
    private readonly Dictionary<int, GeneralPool> _pool = new();


    private bool TryAddPool(GameObject poolItem)
    {
        var id = poolItem.GetInstanceID();

        if (_pool.ContainsKey(id))
            return false;

        _pool.Add(id, new GeneralPool(transform, poolItem));
        return true;
    }

    public GameObject GetItemFromPool(GameObject poolItem)
    {
        TryAddPool(poolItem.gameObject);
        var id = poolItem.GetInstanceID();
        return _pool[id].Pool.Get();
    }
}