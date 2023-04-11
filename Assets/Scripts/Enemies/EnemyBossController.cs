using DG.Tweening;
using UnityEngine;
using Zenject;

public class EnemyBossController : Enemy
{
    [SerializeField] private Transform[] _pathToFollow;
    [SerializeField] private float _moveDuration = 1f;
    [SerializeField] private float _stopDuration = 2f;
    [SerializeField] private float _rotationSpeed = 2f;

    private int _currentPointIndex;
    private bool _isActive;
    private bool _isMoving;
    [Inject] private PlayerController _playerController;
    [Inject] private Vitals _vitals;
    public Vitals Vitals => _vitals;

    private void Update()
    {
        RotateTowardsPlayer();
    }

    private void RotateTowardsPlayer()
    {
        var targetRotation = Quaternion.LookRotation(_playerController.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    private void MoveToNextPoint()
    {
        if (_isMoving) return;

        var nextPoint = _pathToFollow[_currentPointIndex];

        var distance = Vector3.Distance(transform.position, nextPoint.position);
        var duration = distance / _moveDuration;

        var sequence = DOTween.Sequence();

        sequence.Append(transform.DOMove(nextPoint.position, _moveDuration))
            .AppendInterval(_stopDuration)
            .OnComplete(() =>
            {
                _currentPointIndex++;

                if (_currentPointIndex >= _pathToFollow.Length) _currentPointIndex = 0;
                _isMoving = false;
                MoveToNextPoint();
            });

        _isMoving = true;
    }

    public void ActivateBoss()
    {
        _isActive = true;
        MoveToNextPoint();
    }
}