using Cinemachine;
using UnityEngine;
using Zenject;

public class TargetGroupController : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup _tagetGroup;
    [SerializeField] private float _range;
    [SerializeField] private int _maxObservableEnemies;
    [Inject] private EnemyManager _enemyManager;
    [Inject] private PlayerController _playerController;

    private void Update()
    {
        // throw new NotImplementedException();
    }

    private void OnDrawGizmos()
    {
    }

    public void AddTargetToGroup(Transform target, float weight, float radius)
    {
        _tagetGroup.AddMember(target, weight, radius);
    }

    public void RemoveTarget(Transform target, float weight, float radius)
    {
        _tagetGroup.RemoveMember(target);
    }
}