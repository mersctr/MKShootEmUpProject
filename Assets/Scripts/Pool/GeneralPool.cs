using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GeneralPool
{
    private readonly List<GameObject> _createdObjects = new();
    protected Transform _parent;
    protected GameObject _prefab;

    public GeneralPool(Transform parent, GameObject itemPrefab)
    {
        _parent = parent;
        _prefab = itemPrefab;
        Pool = new ObjectPool<GameObject>(CreateItem, GetItem, ReleaseItem, DestroyItem, true, 1, 100);
    }

    public ObjectPool<GameObject> Pool { get; }

    protected virtual void DestroyItem(GameObject item)
    {
        Object.Destroy(item.gameObject);
    }

    protected virtual void ReleaseItem(GameObject item)
    {
        item.gameObject.SetActive(false);
    }

    protected virtual void GetItem(GameObject item)
    {
        item.gameObject.SetActive(true);
    }

    protected virtual GameObject CreateItem()
    {
        var item = Object.Instantiate(_prefab);
        var poolAble = item.GetComponent<IPoolAble>();

        if (poolAble != null) poolAble.ParentPool = Pool;

        _createdObjects.Add(item);
        item.transform.SetParent(_parent);

        return item;
    }

    public void ReleaseAll()
    {
        foreach (var item in _createdObjects)
            Pool.Release(item);
    }
}