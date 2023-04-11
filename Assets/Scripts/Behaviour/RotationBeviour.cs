using UnityEngine;

public class RotationBeviour : MonoBehaviour
{
    [SerializeField] private int _initialRotatioDirection = 1;
    [SerializeField] private float _maxRotationAngle = 30f;
    [SerializeField] private float _speed = 10;
    private int _currentDirRotation;
    private Vector3 _initialDirection;

    private void Awake()
    {
        _currentDirRotation = _initialRotatioDirection;
        _initialDirection = transform.forward;
    }

    private void Update()
    {
        Rotate();
        UpdateRotationDirection();
    }

    private void Rotate()
    {
        transform.Rotate(new Vector3(0, _speed * _currentDirRotation * Time.deltaTime, 0));
    }

    private void UpdateRotationDirection()
    {
        var angle = GetAngle();

        if (_currentDirRotation == 1 && angle >= _maxRotationAngle)
            _currentDirRotation = -1;

        else if (_currentDirRotation == -1 && angle <= -_maxRotationAngle)
            _currentDirRotation = 1;
    }

    private float GetAngle()
    {
        var referenceRight = _initialDirection;
        var targetPosition = transform.forward;

        var angle = Vector3.SignedAngle(referenceRight, targetPosition, Vector3.up);

        return angle;
    }
}