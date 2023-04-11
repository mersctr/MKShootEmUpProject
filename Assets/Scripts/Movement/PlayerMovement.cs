using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    private const float _gravity = 9f;
    [Inject] private PlayerController _actionController;
    [Inject] private CharacterController _characterController;
    private Vector3 _lookPosition;
    private Vector3 _movementDirection;
    [Inject] private PlayerMovementSettingsSO _settings;

    private void Awake()
    {
        _actionController.OnMovementDirectionAction += PlayerActionController_OnMovementDirectionAction;
        _actionController.OnLookAtPositionAction += PlayerActionController_OnLookDirectionAction;
    }

    private void FixedUpdate()
    {
        ApplyGravity();
        LookAt();
        Move();
    }

    private void OnDestroy()
    {
        _actionController.OnMovementDirectionAction -= PlayerActionController_OnMovementDirectionAction;
        _actionController.OnLookAtPositionAction -= PlayerActionController_OnLookDirectionAction;
    }

    private void PlayerActionController_OnMovementDirectionAction(Vector3 direction)
    {
        UpdateMoveDirection(direction);
    }

    private void PlayerActionController_OnLookDirectionAction(Vector3 direction)
    {
        UpdateRotationDirection(direction);
    }

    private void ApplyGravity()
    {
        _characterController.Move(Vector3.down * _gravity * Time.deltaTime);
    }

    private void Move()
    {
        if (_characterController == null)
            return;

        _characterController.Move(_movementDirection * _settings.SpeedMovement * Time.deltaTime);
    }

    private void LookAt()
    {
        transform.LookAt(_lookPosition);
    }

    private void UpdateMoveDirection(Vector3 direction)
    {
        _movementDirection = direction;
    }

    private void UpdateRotationDirection(Vector3 lookPosition)
    {
        _lookPosition = lookPosition;
        _lookPosition.y = transform.position.y;
    }
}