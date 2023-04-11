using UnityEngine;
using Zenject;

public class LifePack : MonoBehaviour, ICollectable
{
    [SerializeField] private int _lifeToCollect = 200;
    [Inject] private PlayerController _playerController;

    private void OnTriggerEnter(Collider other)
    {
        Collect();
        Destroy(gameObject);
    }

    public void Collect()
    {
        _playerController.Vitals.AddLife(200);
    }
}