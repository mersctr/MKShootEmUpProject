using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private bool _useShells;
    [SerializeField] private bool _overrideFireRate;
    [SerializeField] private float _customFireRate;

    [SerializeField] private Weapon[] _currentWeapons;

    private void Awake()
    {
        _currentWeapons = GetComponentsInChildren<Weapon>();
        UpdateShellsUsage();
        UpdateFireRate();
    }

#if UNITY_EDITOR

    private void OnValidate()
    {
        UpdateFireRate();
        UpdateShellsUsage();
    }

#endif

    private void UpdateShellsUsage()
    {
        if (_currentWeapons != null)
            foreach (var weapon in _currentWeapons)
                weapon.SetShellUsage(_useShells);
    }

    private void UpdateFireRate()
    {
        if (_currentWeapons != null)
            foreach (var weapon in _currentWeapons)
                weapon.SetFireRate(_overrideFireRate, _customFireRate);
    }

    public void TryToShoot(bool shoot)
    {
        foreach (var weapon in _currentWeapons)
            weapon?.TryToShoot(shoot);
    }
}