using System.Linq;
using UnityEngine;
using Zenject;

public class BulletManager : MonoBehaviour
{
    [Inject] private SurfaceImpactListSO _imapact;
    [Inject] private PlayerController _playerConntroller;
    [Inject] private PoolSystem _poolSystem;

    public void ShootABullet(Bullet prefab, float force, Transform origin)
    {
        var poolItem = _poolSystem.GetItemFromPool(prefab.gameObject);
        var bullet = poolItem.GetComponent<Bullet>();
        bullet.Initilize(this, 1, origin.position);

        var bulletRigidBody = bullet.Rigidbody;
        //Resetting bullet rigidBody values
        bulletRigidBody.velocity = Vector3.zero;
        bulletRigidBody.angularVelocity = Vector3.zero;
        bulletRigidBody.position = Vector3.zero;
        bulletRigidBody.rotation = Quaternion.identity;
        //Setting new bullet rigidbody values
        bullet.transform.position = origin.position;
        bullet.transform.forward = origin.forward;

        bulletRigidBody.AddForce(origin.forward * force);
    }

    public void CreateMuzzle(GameObject prefab, Transform origin)
    {
        var poolItem = _poolSystem.GetItemFromPool(prefab.gameObject);
        poolItem.transform.SetPositionAndRotation(origin.position, origin.rotation);
    }

    public void CreateImpact(SurfaceType surcafceType, Vector3 position, Quaternion rotation)
    {
        var imapctSettings = _imapact.ImapctSettings.FirstOrDefault(p => p.Surface == surcafceType);

        if (imapctSettings == null)
            return;

        var poolItem = _poolSystem.GetItemFromPool(imapctSettings.ImpactEffct);

        poolItem.transform.position = position;
        poolItem.transform.rotation = rotation;
    }

    public void CreateShells(GameObject prefab, Vector3 position)
    {
        var poolItem = _poolSystem.GetItemFromPool(prefab.gameObject);

        poolItem.transform.position = position;
    }
}