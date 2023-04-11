using UnityEngine;
using UnityEngine.Pool;

public interface IPoolAble
{
    public ObjectPool<GameObject> ParentPool { get; set; }
}