using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask _mouseRaycastTargetsMask;
    [Inject] private Camera _camera;
    [Inject] private PlayerInput _playerInput;
    [Inject] private Vitals _vitals;
    [Inject] private WeaponController _weponController;
    
    public Vitals Vitals => _vitals;

    public Action OnChangeWeaponAction;
    public Action<Vector3> OnLookAtPositionAction;
    public Action<Vector3> OnMovementDirectionAction;
    public Action OnPlayerDied;
    public Action<bool> OnShootAction;

    private void Start()
    {
        _playerInput.Player.Enable();

        _playerInput.Player.Fire.performed += PlayerInput_Fire;
        _playerInput.Player.Fire.canceled += PlayerInput_FireOff;
        _playerInput.Player.ChangeWeapon.performed += PlayerInput_ChangeWeapon;
        _vitals.OnDeath += Vitals_OnDeath;
    }
    
    // If i have subscription to any event or delegates i always try to have function that unsubscribes right under
    // so i can  see whats happening and everything is fine
    private void OnDestroy()
    {
        _playerInput.Player.Fire.performed -= PlayerInput_Fire;
        _playerInput.Player.Fire.canceled -= PlayerInput_FireOff;
        _playerInput.Player.ChangeWeapon.performed -= PlayerInput_ChangeWeapon;
        _vitals.OnDeath -= Vitals_OnDeath;
    }
    
    private void Update()
    {
        UpdateMousePosition();
        UpdateMovementAction();
    }

    private void UpdateMousePosition()
    {
        if (_camera == null)
            return;
        
        var inputValue = _playerInput.Player.Look.ReadValue<Vector2>();
        var ray = _camera.ScreenPointToRay(new Vector3(inputValue.x, inputValue.y, 0));

        if (Physics.Raycast(ray, out var hitPoint, 100f, _mouseRaycastTargetsMask))
        {
            OnLookAtPositionAction?.Invoke(hitPoint.point);
            Debug.DrawLine(transform.position, hitPoint.point);
        }
    }
    
    private void UpdateMovementAction()
    {
        if (_playerInput == null)
            return;
        
        var inputValue = _playerInput.Player.Move.ReadValue<Vector2>();
        var direction = new Vector3(inputValue.x, 0, inputValue.y).normalized;

        OnMovementDirectionAction?.Invoke(direction);
    }

    private void Vitals_OnDeath()
    {
        gameObject.SetActive(false);
        OnPlayerDied?.Invoke();
    }

    private void PlayerInput_Fire(InputAction.CallbackContext context)
    {
        _weponController.TryToShoot(true);
    }

    private void PlayerInput_FireOff(InputAction.CallbackContext obj)
    {
        _weponController.TryToShoot(false);
    }

    private void PlayerInput_ChangeWeapon(InputAction.CallbackContext obj)
    {
        OnChangeWeaponAction?.Invoke();
    }
}